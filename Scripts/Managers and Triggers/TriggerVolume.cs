using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolume : MonoBehaviour
{
    bool entered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TriggerEntered(bool enter)
    {
        entered = enter;
    }

    public bool GetEntered()
    {
        return entered;
    }
}
