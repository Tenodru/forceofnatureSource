using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [Header("Loot Attributes")]
    [Tooltip("List of items this NPC can drop.")]
    [SerializeField] Item[] itemList;
    [Tooltip("The drop chances for each item in the above list of items.")]
    [SerializeField] float[] dropChances;

    Quaternion rotation;
    bool wasLootDropped = false;

    // Start is called before the first frame update
    void Start()
    {
        rotation = new Quaternion(0, 0, 0, 0);
    }

    public void DropLoot(Transform target)
    {
        //If this method has already run, do not run it again.
        if (wasLootDropped)
        {
            return;
        }

        if (itemList.Length == 0)
        {
            return;
        }
        else if (dropChances.Length == 0)
        {
            return;
        }

        wasLootDropped = true;

        for (int i = 0; i < itemList.Length; i++)
        {
            float roll = Random.Range(0f, 1f);
            if (roll < dropChances[0])
            {
                //If dropChance was met, drop the item.
                Vector3 dropPos = new Vector3(target.position.x, target.position.y + 5, target.position.z);
                Instantiate(itemList[0].GetObject(), dropPos, rotation);
                Debug.Log(itemList[0].GetItemName() + " was dropped.");
            }
        }
    }
}
