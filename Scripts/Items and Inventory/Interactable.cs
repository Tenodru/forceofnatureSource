using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool interacted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClicked()
    {
        Debug.Log(name + " was clicked.");
        interacted = true;
    }

    public bool GetInteracted()
    {
        return interacted;
    }
}
