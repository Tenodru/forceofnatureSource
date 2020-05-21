using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An object that can be picked up. Associated with an Item object.
/// </summary>
public class Pickup : MonoBehaviour
{
    #region Pickup Attributes
    [Header("Pickup Attributes")]
    [Tooltip("What this pickup will give the player, if it is an item.")]
    [SerializeField] Item item;
    [Tooltip("The number of score points gained by the player for picking up this item.")]
    [SerializeField] int itemScore;
    [Tooltip("The sound that plays when this item is picked up.")]
    public AudioClip pickupSound = null;
    #endregion

    #region Events
    [Header("Events")]
    [Tooltip("Invoked when this item is picked up and collected.")]
    public UnityEvent OnPickup;
    #endregion

    #region Other References
    Inventory inventory;
    StatManager statManager;
    #endregion

    private void Awake()
    {
        if (OnPickup == null)
            OnPickup = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        statManager = StatManager.instance;
    }

    /// <summary>
    /// Called when a collector collides with this object.
    /// </summary>
    public void PickupItem()
    {
        if(inventory.Add(item))
        {
            //Item is added to inventory and the physical pickup is removed.
            statManager.ChangeScore(itemScore);
            OnPickup.Invoke();
            Destroy(gameObject);
        }
        else
        {
            //Not enough space in inventory to add item.
            inventory.NotEnoughSpace();
        }
    }
}
