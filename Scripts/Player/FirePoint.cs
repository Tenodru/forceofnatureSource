using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    #region Singleton
    public static FirePoint instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void OnWeaponFired();
    public OnWeaponFired onWeaponFired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
