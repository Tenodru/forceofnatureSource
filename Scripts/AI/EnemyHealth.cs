using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Health system handler for enemies.
/// </summary>
public class EnemyHealth : Health
{
    StatManager statManager;
    LootDrop lootDrop;

    [HideInInspector]
    public AIAnimationController animController;

    private void Start()
    {
        statManager = StatManager.instance;
        lootDrop = GetComponent<LootDrop>();

        animController = GetComponent<AIAnimationController>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        //Play hurt sequence via animator.
        //Update this character's health display.
    }

    public override void Die()
    {
        base.Die();

        GetComponent<AI>().state = AI.CreatureState.Dead;

        if (GetComponent<EnemyWalkerAI>().navMesh != null)
            GetComponent<EnemyWalkerAI>().navMesh.SetDestination(transform.position);

        Debug.Log("Dropped loot.");

        lootDrop.DropLoot(transform);
        statManager.ChangeEnemiesKilled(1);
        statManager.ChangeCurrentEnemyCount(-1);

        GetComponent<SoundManager>().PlayDeathSound();

        //Play death animation sequence and turn off all relevant components.
        animController.PlayDeath();
        GetComponent<AI>().enabled = false;
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }

        if (GetComponent<StatsDisplay>() != null)
        {
            GetComponent<StatsDisplay>().healthDisplay.IsEnabled(false);
            GetComponent<StatsDisplay>().enabled = false;
        }

        //Update any stats and displays that change on character death.

        StartCoroutine(RemoveEntity(5));
    }

    /// <summary>
    /// Removes this entity after the specified period of time.
    /// </summary>
    /// <param name="time">How long this entity will remain after death.</param>
    /// <returns></returns>
    IEnumerator RemoveEntity(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
