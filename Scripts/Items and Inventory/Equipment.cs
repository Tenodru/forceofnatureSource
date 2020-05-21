using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Framework for Equipment objects.
/// </summary>
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [SerializeField] EquipmentSlot equipSlot;
    [SerializeField] int armorModifier;
    [SerializeField] int damageModifier;

    /// <summary>
    /// Uses the equipment.
    /// </summary>
    public override void Use()
    {
        base.Use();
    }

    /// <summary>
    /// Gets this equipment's equipment slot.
    /// </summary>
    /// <returns></returns>
    public EquipmentSlot GetEquipSlot()
    {
        return equipSlot;
    }

    /// <summary>
    /// Gets this equipment's armor modifier.
    /// </summary>
    /// <returns></returns>
    public int GetArmorModifier()
    {
        return armorModifier;
    }

    /// <summary>
    /// Gets this equipment's damage modifer.
    /// </summary>
    /// <returns></returns>
    public int GetDamageModifier()
    {
        return damageModifier;
    }
}

/// <summary>
/// Outline for equipment slots.
/// </summary>
public enum EquipmentSlot { Head, Chest, Arms, Legs, Feet }