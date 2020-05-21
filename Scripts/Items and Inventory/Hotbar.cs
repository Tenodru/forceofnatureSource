using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    #region Singleton
    public static Hotbar instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Hotbar found!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnHotbarItemChanged();
    public OnHotbarItemChanged onHotbarItemChangedCallback;

    [SerializeField] int space = 7;

    public List<Item> items = new List<Item>();

    public virtual bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room in inventory.");
                return false;
            }
            items.Add(item);
            if (onHotbarItemChangedCallback != null)
                onHotbarItemChangedCallback.Invoke();
        }
        return true;
    }

    public virtual void Remove(Item item)
    {
        items.Remove(item);
        if (onHotbarItemChangedCallback != null)
            onHotbarItemChangedCallback.Invoke();
    }

    public virtual void ChangeInventorySpace(int amount)
    {
        space += amount;
    }
}
