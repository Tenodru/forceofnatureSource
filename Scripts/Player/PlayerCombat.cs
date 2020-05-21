using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A combat system handler for players.
/// </summary>
public class PlayerCombat : Combat
{
    #region Public References
    [Header("References")]
    [Tooltip("The layers on which the player combat system will check for enemies when attacking.")]
    [SerializeField] LayerMask enemyLayers;
    #endregion

    #region Other References
    SoundManager soundManager;
    EquipmentManager equipManager;
    WeaponManager weaponManager;
    Inventory inventory;
    Weapon activeWeapon = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        OnStart();

        if (GetComponent<SoundManager>() != null)
        {
            soundManager = GetComponent<SoundManager>();
        }

        equipManager = EquipmentManager.instance;
        weaponManager = WeaponManager.instance;
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.GetInventory().Count > 0)
        {
            Debug.Log("Active weapon set to " + inventory.GetInventory()[inventory.GetActiveSlot()].GetItemName());
            activeWeapon = (Weapon)inventory.GetInventory()[inventory.GetActiveSlot()];
            stats.currentAttackSpeed *= ((Weapon)inventory.GetInventory()[inventory.GetActiveSlot()]).GetAttackSpeedModifier();
        }

        if (Input.GetButton("Fire1"))
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / (stats.currentAttackSpeed * activeWeapon.GetAttackSpeedModifier());
            }
        }
    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log("Active weapon is " + activeWeapon.GetItemName());

        RaycastHit hit;

        if (activeWeapon != null)
        {
            if (activeWeapon.GetWeaponSlot() == WeaponSlot.Melee)
            {
                activeWeapon.UseWeapon(stats.currentDamage);
                soundManager.PlayMeleeAttackSound();
            }
            else if (activeWeapon.GetWeaponSlot() == WeaponSlot.BasicRanged)
            {
                activeWeapon.UseWeapon(stats.currentDamage);
                soundManager.PlayRangedAttackSound();
            }
        }
        else
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50, enemyLayers))
            {
                hit.transform.GetComponent<Health>().TakeDamage(stats.currentDamage);
            }
        }
    }
}
