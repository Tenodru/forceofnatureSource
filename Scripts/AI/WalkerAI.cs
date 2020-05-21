using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base AI class for walkers.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class WalkerAI : AI
{
    [HideInInspector] public NavMeshAgent navMesh;
    [HideInInspector] public Rigidbody rb;

    /// <summary>
    /// Assignments and code called on start.
    /// </summary>
    public override void OnStart()
    {
        base.OnStart();

        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Uses RandomNavSphere to determine where this creature can roam.
    /// </summary>
    /// <param name="origin">Starting position.</param>
    /// <param name="dist">Maximum distance to wander.</param>
    /// <param name="navMask">NavMesh mask to read.</param>
    public virtual void Wander(Vector3 origin, float dist, int navMask)
    {
        #region Animation
        animController.StartWalking();
        #endregion

        //If this creature is dead, it will stop moving.
        if (state == CreatureState.Dead)
        {
            navMesh.SetDestination(transform.position);
            return;
        }

        state = CreatureState.Wandering;
        Vector3 newPos = RandomNavSphere(origin, dist, navMask);
        navMesh.SetDestination(newPos);
        timer = 0;

        //Calculates the distance between the creature's current and new positions.
        float distance = Vector3.Distance(transform.position, newPos);

        //Calculates the time it will take for the creature to reach its destination.
        float timeTillDestination = distance / stats.currentMoveSpeed;

        //Sets the creature's state to Idle once it has reached its destination.
        StartCoroutine(ChangeState(timeTillDestination, CreatureState.Idle));
    }

    /// <summary>
    /// Draws a sphere around this object and returns a point within that sphere.
    /// </summary>
    /// <param name="origin">Starting position.</param>
    /// <param name="dist">Radius of sphere.</param>
    /// <param name="navMask">NavMesh mask to read.</param>
    /// <returns></returns>
    public Vector3 RandomNavSphere(Vector3 origin, float dist, int navMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, navMask);

        return navHit.position;
    }

    /// <summary>
    /// Rotates this creature to look at target object.
    /// </summary>
    /// <param name="target"></param>
    public void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    /// <summary>
    /// Changes CreatureState to specified state after specified period of time.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="newState"></param>
    /// <returns></returns>
    public IEnumerator ChangeState(float time, CreatureState newState)
    {
        yield return new WaitForSeconds(time);
        state = newState;

        #region Animation
        animController.StopWalking();
        #endregion
    }
}
