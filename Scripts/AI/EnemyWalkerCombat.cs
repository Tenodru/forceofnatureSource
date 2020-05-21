using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A combat system handler for enemy AI.
/// </summary>
public class EnemyWalkerCombat : Combat
{
    AI ai;
    AIAnimationController animController;

    //Durations of relevant combat Actions.
    float basicAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();

        ai = GetComponent<AI>();
        animController = GetComponent<AIAnimationController>();

        basicAttackTime = animController.GetActionDuration(AnimationActionType.BasicAttack);
    }

    /// <summary>
    /// Attacks when certain conditions are met.
    /// </summary>
    public override void Attack()
    {
        base.Attack();

        //If this creature has waited long enough to attack...
        if (Time.time >= nextAttackTime)
        {
            //...set its state to Attacking and deal damage to the target.
            ai.state = AI.CreatureState.Attacking;
            animController.PlayBasicAttack();

            StartCoroutine(DealDamage(stats.currentAttackDelay));
            nextAttackTime = Time.time + 1f / stats.currentAttackSpeed;

            //After attacking, set this creature's state to its resting state before attacking again.
            StartCoroutine(AttackRest(AI.CreatureState.Waiting, basicAttackTime));
        }
    }

    /// <summary>
    /// Deals damage to the target after the specified amount of time.
    /// </summary>
    /// <param name="time">The time that damage should be dealt to the target.</param>
    /// <returns></returns>
    public IEnumerator DealDamage(float time)
    {
        yield return new WaitForSeconds(time);

        if (ai.state == AI.CreatureState.Waiting)
            ai.currentTarget.transform.GetComponent<Health>().TakeDamage(stats.currentDamage);
    }

    /// <summary>
    /// Sets this creature's State to the specified State after the specified period of time, after attacking..
    /// </summary>
    /// <param name="state">The state this creature will enter after each attack.</param>
    /// <param name="time">How long the attack takes to finish.</param>
    /// <returns></returns>
    public IEnumerator AttackRest(AI.CreatureState state, float time)
    {
        yield return new WaitForSeconds(time);

        ai.state = state;
    }
}
