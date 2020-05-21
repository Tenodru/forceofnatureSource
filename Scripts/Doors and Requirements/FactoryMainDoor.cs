using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMainDoor : Door
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

        if (GetCloseRequirement().RequirementMet())
        {
            Close();
        }
    }

    public override void Open()
    {
        base.Open();

        //Animate door to open
        anim.SetTrigger("Door Unlocked");
    }

    public override void Close()
    {
        base.Close();

        //Animate door to close
        anim.SetTrigger("Factory Entered");
    }
}
