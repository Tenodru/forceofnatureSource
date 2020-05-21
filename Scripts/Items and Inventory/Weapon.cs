using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Framework for Weapon Items.
/// </summary>
[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    #region Weapon Attributes
    [Header("Weapon Attributes")]
    [Tooltip("The type of weapon this weapon is.")]
    [SerializeField] WeaponSlot weaponSlot;
    [Tooltip("How much additional damage the wielder of this weapon will deal while it is active.")]
    [SerializeField] int damageModifier;
    [Tooltip("The percentage by which character attack speed is affected by this weapon.")]
    [SerializeField] float attackSpeedModifier = 1;
    [Tooltip("The attack range of this weapon. Only applied if the weapon is melee.")]
    [SerializeField] int attackRange = 0;
    #endregion

    #region References
    [Header("References")]
    [Space]
    [Tooltip("The ammunition used by this weapon. Only applied if the weapon is ranged.")]
    [SerializeField] GameObject ammo = null;
    [Tooltip("The layers on which this weapon will check for enemies when attacking. Primarily for melee weapons.")]
    [SerializeField] LayerMask enemyLayers;
    #endregion

    FirePoint firePoint;

    public void OnEnable()
    {
        if (weaponSlot != WeaponSlot.Melee)
        {
            attackRange = 0;
        }
    }

    /// <summary>
    /// Uses the weapon.
    /// </summary>
    public override void UseWeapon(int damage)
    {
        base.UseWeapon(damage);
        firePoint = FirePoint.instance;

        if (weaponSlot == WeaponSlot.Melee)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, GetAttackRange(), enemyLayers))
            {
                hit.transform.GetComponent<Health>().TakeDamage(damage + GetDamageModifier());
            }
        }

        if (weaponSlot == WeaponSlot.BasicRanged)
        {
            Debug.Log("Fired " + name);
            SpawnProjectile(ammo);
        }
    }

    public override void OnActive(Camera mainCam)
    {
        base.OnActive(mainCam);
    }

    public void OnInactive()
    {
        GameObject[] HUDModels = GameObject.FindGameObjectsWithTag("HUDModel");
        foreach(GameObject model in HUDModels)
        {
            Destroy(model);
        }
    }

    [System.Obsolete("Obsolete method. Check ItemType instead.")]
    public override bool IsWeapon()
    {
        return true;
    }

    /// <summary>
    /// Spawns the projectile associated with this weapon.
    /// </summary>
    /// <param name="ammo">This weapon's projectile.</param>
    void SpawnProjectile(GameObject ammo)
    {
        if (firePoint == null)
        {
            Debug.Log("Firepoint hasn't been referenced!");
            return;
        }

        Instantiate(ammo, firePoint.transform.position, Camera.main.transform.rotation);
    }

    /// <summary>
    /// Gets this weapon's weapon slot.
    /// </summary>
    /// <returns></returns>
    public WeaponSlot GetWeaponSlot()
    {
        return weaponSlot;
    }

    /// <summary>
    /// Gets this weapon's damage modifer.
    /// </summary>
    /// <returns></returns>
    public int GetDamageModifier()
    {
        return damageModifier;
    }

    /// <summary>
    /// Gets this weapon's attack speed modifier.
    /// </summary>
    /// <returns></returns>
    public float GetAttackSpeedModifier()
    {
        return attackSpeedModifier;
    }

    /// <summary>
    /// Gets this weapon's attack range.
    /// </summary>
    /// <returns></returns>
    public int GetAttackRange()
    {
        return attackRange;
    }
}

/// <summary>
/// Outline for weapon slots.
/// </summary>
public enum WeaponSlot { Melee, BasicRanged }

