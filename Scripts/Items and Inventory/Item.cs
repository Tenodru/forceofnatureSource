using UnityEngine;

/// <summary>
/// Framework for Item objects.
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    #region Item Attributes
    [Header("Item Attributes")]
    [Tooltip("This item's name.")]
    public new string name = "New Item";      //Name of item
    [Tooltip("What kind of item this is.")]
    public ItemType itemType;
    [Tooltip("The icon for this item.")]
    public Sprite icon = null;                          //Item icon
    [Tooltip("Is this item a default item?")]
    public bool isDefaultItem = false;                  //Is the item a default item?
    [Tooltip("The physical model for this item.")]
    public GameObject item;
    [Tooltip("The model to be displayed on the HUD when this weapon is active.")]
    public GameObject HUDModel = null;
    #endregion

    /// <summary>
    /// The active HUD model object.
    /// </summary>
    public GameObject m_HUDModel;

    /// <summary>
    /// Overridable method. Uses the item.
    /// </summary>
    public virtual void Use()
    {
        Debug.Log("Using " + name);

        m_HUDModel.GetComponent<ItemAnimationController>().PlayUse();
    }

    /// <summary>
    /// Overridable method. Uses the weapon given a base damage stat.
    /// </summary>
    public virtual void UseWeapon(int damage)
    {
        Debug.Log("Using " + name);

        m_HUDModel.GetComponent<ItemAnimationController>().PlayAttack();
    }

    /// <summary>
    /// Displays this item's HUD model when it is switched to, if it has one.
    /// </summary>
    /// <param name="mainCam">The game's main camera.</param>
    public virtual void OnActive(Camera mainCam)
    {
        if (HUDModel != null)
        {
            m_HUDModel = Instantiate(GetHUDModel(), mainCam.transform);
        }
    }

    [System.Obsolete("Obsolete method. Check ItemType instead.")]
    /// <summary>
    /// Checks if this Item is a Weapon.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsWeapon()
    {
        return false;
    }

    [System.Obsolete("Obsolete method. Check ItemType instead.")]
    /// <summary>
    /// Checks if this Item is an Equipment.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsEquipment()
    {
        return false;
    }

    /// <summary>
    /// Gets this item's name.
    /// </summary>
    /// <returns></returns>
    public string GetItemName()
    {
        return name;
    }

    public GameObject GetObject()
    {
        return item;
    }

    public GameObject GetHUDModel()
    {
        return HUDModel;
    }

    /// <summary>
    /// Removes item from inventory.
    /// </summary>
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

/// <summary>
/// The Type of Item this is.
/// </summary>
[System.Serializable]
public enum ItemType
{
    Item, Consumable, Weapon, Equipment
}
