using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A requirement for something else to happen.
/// </summary>
public class CloseRequirement : MonoBehaviour
{
    [Tooltip("Enable this if the player should kill a certain number of enemies to meet the requirement.")]
    [SerializeField] bool req_enemiesKilled = false;
    [Tooltip("The required number of enemies killed.")]
    [SerializeField] int enemiesKilled;
    [Tooltip("Enable this if the player should find an item to meet the requirement.")]
    [SerializeField] bool req_needItem = false;
    [Tooltip("The required item.")]
    [SerializeField] Item itemReq;
    [Tooltip("Enable this if the player should enter an trigger volume to meet the requirement.")]
    [SerializeField] bool req_enterTrigger = false;
    [Tooltip("The trigger volume to be entered.")]
    [SerializeField] TriggerVolume trigger;
    [Tooltip("Enable this if the player should interact with an object to meet the requirement.")]
    [SerializeField] bool req_interact = false;
    [Tooltip("The object to be interacted with.")]
    [SerializeField] Interactable interactable;

    bool reqMet;
    bool reqMet_enemiesKilled = false;
    bool reqMet_needItem = false;
    bool reqMet_enterTrigger = false;
    bool reqMet_interact = false;

    int reqNum;                 //The number of requirements needed.
    int reqNumMet;              //The number of requirements met.

    StatManager statManager;
    Inventory inventory;

    private void Awake()
    {
        reqNum = 0;
        reqMet = false;
        CountReqs();
    }

    private void Start()
    {
        statManager = StatManager.instance;
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    public void Update()
    {
        if (req_enemiesKilled)
        {
            ReqEnemiesKilled();
        }
        if (req_needItem)
        {
            ReqNeedItem();
        }
        if (req_enterTrigger)
        {
            ReqTriggerEntered();
        }
        if (req_interact)
        {
            ReqInteracted();
        }

        if (reqNumMet == reqNum)
        {
            reqMet = true;
        }
    }

    void CountReqs()
    {
        if (req_enemiesKilled == true)
            reqNum++;
        if (req_needItem == true)
            reqNum++;
        if (req_enterTrigger == true)
            reqNum++;
        if (req_interact == true)
            reqNum++;
    }

    void ReqEnemiesKilled()
    {
        if (reqMet_enemiesKilled)
        {
            return;
        }

        if (statManager.GetEnemiesKilled() >= enemiesKilled)
        {
            //If the required number of enemies killed has been met....

            reqMet_enemiesKilled = true;
            reqNumMet++;
        }
    }

    void ReqNeedItem()
    {
        if (reqMet_needItem)
        {
            return;
        }

        foreach (Item item in inventory.GetInventory())
        {
            if (item == itemReq)
            {
                //If the specified item was found in the player's inventory....

                reqMet_needItem = true;
                reqNumMet++;
                return;
            }
        }
    }

    void ReqTriggerEntered()
    {
        if (reqMet_enterTrigger)
        {
            return;
        }

        if (trigger.GetEntered())
        {
            //If the trigger was entered....

            reqMet_enterTrigger = true;
            reqNumMet++;
        }
    }

    void ReqInteracted()
    {
        if (reqMet_interact)
        {
            return;
        }

        if (interactable.GetInteracted())
        {
            //If the interactable was interacted with....

            reqMet_interact = true;
            reqNumMet++;
        }
    }

    public bool RequirementMet()
    {
        return reqMet;
    }
}
