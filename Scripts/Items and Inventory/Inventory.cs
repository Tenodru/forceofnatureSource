using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    [Header("Inventory Attributes")]
    [SerializeField] int space = 7;
    [SerializeField] List<Item> items = new List<Item>();
    [SerializeField] List<Weapon> weapons = new List<Weapon>();

    int activeSlot = 0;
    int previousSlot = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeSlot = 0;
            previousSlot = 0;
            if (weapons[0] != null)
            {
                DestroyHUDModels();

                Debug.Log("Switched to weapon 1.");
                weapons[0].OnActive(Camera.main);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weapons.Count < 2)
            {
                Debug.Log("No weapon to switch to.");
            }
            else
            {
                activeSlot = 1;

                DestroyHUDModels();

                Debug.Log("Switched to weapon 2.");
                weapons[1].OnActive(Camera.main);
            }
        }
    }

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                return false;
            }

            if (item.itemType == ItemType.Weapon)
            {
                weapons.Add((Weapon)item);
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
            items.Add(item);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

        }
        return true;
    }

    public void UseItem(int damage)
    {
        if (items[activeSlot].itemType == ItemType.Weapon)
        {
            items[activeSlot].UseWeapon(damage);
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void NotEnoughSpace()
    {
        Debug.Log("Not enough room in inventory.");
    }

    public void ShowDefaultWeapon()
    {
        if (weapons[0] == null)
        {
            return;
        }
        weapons[0].OnActive(Camera.main);
    }

    public void ChangeInventorySpace(int amount)
    {
        space += amount;
    }

    public int CheckForItems(Item item)
    {
        int itemCount = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == item)
            {
                itemCount++;
            }
        }

        return itemCount;
    }

    public int GetActiveSlot()
    {
        return activeSlot;
    }

    /// <summary>
    /// Returns a list of items representing what is currently in the player's inventory.
    /// </summary>
    /// <returns></returns>
    public List<Item> GetInventory()
    {
        return items;
    }

    /// <summary>
    /// Destroys all HUD models.
    /// </summary>
    void DestroyHUDModels()
    {
        GameObject[] HUDModels = GameObject.FindGameObjectsWithTag("HUDModel");
        foreach (GameObject model in HUDModels)
        {
            Destroy(model);
        }
    }
}