using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gateway1 : Door
{
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetOpenRequirement().RequirementMet())
        {
            Open();
        }
    }

    public override void Open()
    {
        base.Open();

        //Animate gateway to fall down or something
        foreach (Collider col in GetComponents<Collider>())
            col.enabled = false;

        anim.SetTrigger("OpenDoor");
    }
}
