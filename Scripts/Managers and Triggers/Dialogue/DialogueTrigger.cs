using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public bool trigger = false;
    [HideInInspector] public bool wasAlreadyTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            trigger = true;
            Debug.Log("Dialogue triggered.");
        }
    }
}
