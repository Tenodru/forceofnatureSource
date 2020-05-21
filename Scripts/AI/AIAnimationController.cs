using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages and triggers animation sequences for this character.
/// </summary>
[RequireComponent(typeof(Animator))]
public class AIAnimationController : MonoBehaviour
{
    #region Animator Triggers and Parameters
    [Header("Animator Triggers and Parameters")]
    [Tooltip("The name of the ReachedTarget boolean parameter. This stops this AI's moving animation.")]
    public string reachedTargetParameter;
    [Tooltip("The name of the FoundTarget boolean parameter. This triggers this AI's moving animation.")]
    public string foundTargetParameter;
    [Tooltip("The name of the BasicAttack trigger parameter. This triggers this AI's basic attack animation.")]
    public string basicAttackParameter;
    [Tooltip("The name of the IsDead boolean parameter. This triggers this AI's death animation.")]
    public string isDeadParameter;
    [Tooltip("The name of the Dead trigger parameter. This triggers this AI's death animation.")]
    public string deathParameter;
    [Tooltip("The name of the Hurt trigger parameter. This triggers this AI's hurt animation.")]
    public string hurtParameter;
    #endregion

    [Header("Animation Actions")]
    [Tooltip("A list of all of this character's Actions and their durations.")]
    public AnimationAction[] actions;

    #region References
    Animator anim;
    AI ai;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ai = GetComponent<AI>();
    }

    /// <summary>
    /// Checks if the specified Action is included in this character's Animation Action list.
    /// </summary>
    /// <param name="actionType">The Action to search for.</param>
    /// <returns>Whether the Action is in the list.</returns>
    public bool GetAction(AnimationActionType actionType)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionType == actionType)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Gets the duration of the specified Action. If it does not exist, returns 0.
    /// </summary>
    /// <param name="actionType">The Action to search for.</param>
    /// <returns>The duration of the specified Action.</returns>
    public float GetActionDuration(AnimationActionType actionType)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionType == actionType)
            {
                return actions[i].duration;
            }
        }
        return 0;
    }

    #region Animation Sequence Play Methods
    /// <summary>
    /// Starts walking sequence.
    /// </summary>
    public void StartWalking()
    {
        if (HasFoundTargetParam())
            anim.SetBool(foundTargetParameter, true);
        if (HasReachedTargetParam())
            anim.SetBool(reachedTargetParameter, false);
    }

    /// <summary>
    /// Stops walking sequence.
    /// </summary>
    public void StopWalking()
    {
        if (HasReachedTargetParam())
            anim.SetBool(reachedTargetParameter, true);
    }

    /// <summary>
    /// Plays basic attack animation.
    /// </summary>
    public void PlayBasicAttack()
    {
        if (HasBasicAttackParam())
            anim.SetTrigger(basicAttackParameter);
    }

    /// <summary>
    /// Plays hurt animation.
    /// </summary>
    public void PlayHurt()
    {
        if (HasHurtParam())
        {
            anim.SetTrigger(hurtParameter);
        }
    }

    /// <summary>
    /// Plays death animation.
    /// </summary>
    public void PlayDeath()
    {
        if (HasIsDeadParam() && HasDeathParam())
        {
            anim.SetBool(isDeadParameter, true);
            anim.SetTrigger(deathParameter);
        }
    }
    #endregion

    #region Conditional Parameter Checks
    /// <summary>
    /// Checks if the animator has a ReachedTarget parameter.
    /// </summary>
    /// <returns></returns>
    public bool HasReachedTargetParam()
    {
        if (string.IsNullOrEmpty(reachedTargetParameter) == true)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the animator has a FoundTarget parameter.
    /// </summary>
    /// <returns></returns>
    public bool HasFoundTargetParam()
    {
        if (string.IsNullOrEmpty(foundTargetParameter) == true)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the animator has a BasicAttack parameter.
    /// </summary>
    /// <returns></returns>
    public bool HasBasicAttackParam()
    {
        if (string.IsNullOrEmpty(basicAttackParameter) == true)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the animator has an IsDead parameter.
    /// </summary>
    /// <returns></returns>
    public bool HasIsDeadParam()
    {
        if (string.IsNullOrEmpty(isDeadParameter) == true)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the animator has a Death parameter.
    /// </summary>
    /// <returns></returns>
    public bool HasDeathParam()
    {
        if (string.IsNullOrEmpty(deathParameter) == true)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the animator has a Hurt parameter.
    /// </summary>
    /// <returns></returns>
    public bool HasHurtParam()
    {
        if (string.IsNullOrEmpty(hurtParameter) == true)
        {
            return false;
        }
        return true;
    }
    #endregion
}

/// <summary>
/// An action type and its duration.
/// </summary>
[System.Serializable]
public class AnimationAction
{
    /// <summary>
    /// The type of Action.
    /// </summary>
    [Tooltip("The type of Action.")]
    public AnimationActionType actionType;

    /// <summary>
    /// The duration of this Action.
    /// </summary>
    [Tooltip("The duration of this Action.")]
    public float duration;

    /// <summary>
    /// Creates a new AnimationAction with the specified ActionType and duration.
    /// </summary>
    /// <param name="newType">The ActionType.</param>
    /// <param name="time">The duration of this Action.</param>
    public AnimationAction(AnimationActionType newType, float time)
    {
        actionType = newType;
        duration = time;
    }
}

/// <summary>
/// A type of Action.
/// </summary>
public enum AnimationActionType
{
    BasicAttack, SecondaryAttack, SuperAttack, Defend
}
