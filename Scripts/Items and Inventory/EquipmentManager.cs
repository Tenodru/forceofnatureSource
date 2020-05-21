using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;                                       //Callback method called whenever equipment is changed.

    int activeSlot = 0;
    Equipment[] equipmentSlots;
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        equipmentSlots = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.GetEquipSlot();            //Determines equipment slot for newItem
        Equipment oldItem = null;
        if (equipmentSlots[slotIndex] != null)                  //If the current slot has an item...
        {
            oldItem = equipmentSlots[slotIndex];                //...set old item to that item...
            inventory.Add(oldItem);                             //...and add it back to the inventory.
        }
        if (onEquipmentChanged != null)                         //Invoke callback method when item is equipped.
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        equipmentSlots[slotIndex] = newItem;                    //Equips newItem in slot [slotIndex]
    }
}
