using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyWalkerCombat))]
public class EnemyWalkerAI : WalkerAI
{
    EnemyHealth health;
    EnemyWalkerCombat combat;
    Entity curTarget;

    float timePassed = 0;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();

        allegiance = CreatureAllegiance.Hostile;
        combat = GetComponent<EnemyWalkerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        //If this creature is dead, it will stop moving.
        if (state == CreatureState.Dead)
        {
            navMesh.SetDestination(transform.position);
            Debug.Log(state);
            return;
        }

        if (GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            if (1f + timePassed <= Time.time)
            {
                GetComponent<SoundManager>().PlayWalkSound();
                timePassed = Time.time;
            }
        }

        curTarget = currentTarget;
        timer += Time.deltaTime;
        navMesh.speed = stats.currentMoveSpeed;

        //If an object of interest is detected...
        if (Detect() != null)
        {
            //...set the creature's state to SeekTarget and its currentTarget to the detected Entity...
            state = CreatureState.SeekTarget;
            currentTarget = Detect();

            //...and move to the object.
            GoToTarget(Detect().transform);
            Debug.Log("Detected " + Detect().transform.name);
        }
        //Otherwise, roam the area.
        else if (timer >= GetWanderTime())
        {
            if (state == CreatureState.Idle)
            {
                Wander(transform.position, wanderRadius, -1);
            }
        }

        //Regardless of other commands, if no objects of interest are detected, set state back to Idle and currentTarget to null.
        if (Detect() == null)
        {
            state = CreatureState.Idle;
            currentTarget = null;
        }
    }

    /// <summary>
    /// Moves to the specified target.
    /// </summary>
    /// <param name="target">The target to move to.</param>
    void GoToTarget(Transform target)
    {
        //If this creature is dead, it will stop moving.
        if (state == CreatureState.Dead)
        {
            navMesh.SetDestination(transform.position);
            Debug.Log(state);
            return;
        }

        //Calculates the distance between the creature's current and new positions.
        float distance = Vector3.Distance(transform.position, target.position);

        //Calculates the time it will take for the creature to reach its destination.
        float timeTillDestination = distance / stats.currentMoveSpeed;

        //Sets relevant animation parameters.
        animController.StartWalking();

        //Face the target and move towards it.
        FaceTarget(target);
        navMesh.SetDestination(target.position);

        //If the distance between this character and the target is less than its stopping distance...
        if (distance <= stopDistance)
        {
            //Sets relevant animation parameters.
            if (animController.HasReachedTargetParam())
                anim.SetBool(animController.reachedTargetParameter, true);

            //Stops movement.
            navMesh.SetDestination(transform.position);

            //...decide this creature's intent with the target.
            state = CreatureState.Waiting;
            DecideIntent(target);
        }
    }

    /// <summary>
    /// Reads info about target and decides its next course of action.
    /// </summary>
    /// <param name="target"></param>
    void DecideIntent(Transform target)
    {
        //If the target is a Player...
        if (target.GetComponent<Stats>().entityType == EntityType.Player)
        {
            //...attack.
            combat.Attack();
        }
    }
}
