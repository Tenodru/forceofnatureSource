using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    StatManager statManager;

    private void Start()
    {
        statManager = StatManager.instance;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        statManager.ChangePlayerHealth(-damage);
        GetComponent<SoundManager>().PlayHurtSound(0.1f);

        //Play hurt sound
        //Play hurt animation on UI
        //Update player health display
    }

    public override void Die()
    {
        base.Die();

        //Play death sound
        //Play death animation sequence
        //Move camera to third-person
        //Play game over sequence
    }
}