using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Singleton
    public static WeaponManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void OnWeaponChanged(Equipment newItem, Equipment oldItem);
    public OnWeaponChanged onWeaponChanged;                                         //Callback method called whenever weapon is switched.
    
    Weapon[] weaponSlots;
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(WeaponSlot)).Length;
        weaponSlots = new Weapon[numSlots];
    }

    private void Update()
    {
        
    }

    public void SwitchSlots()
    {
        
    }

    public Weapon GetCurrentWeapon(int slot)
    {
        Debug.Log("Weapon in slot " + slot);
        return weaponSlots[slot];
    }
}
