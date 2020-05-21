using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages and triggers animation sequences for this item.
/// </summary>
[RequireComponent(typeof(Animator))]
public class ItemAnimationController : MonoBehaviour
{
    #region Animator Triggers and Parameters
    [Header("Animator Triggers and Parameters")]
    [Tooltip("The name of the Use trigger parameter. This triggers this item's Use animation.")]
    public string useParameter;
    [Tooltip("The name of the Attack trigger parameter. This triggers this item's Attack animation.")]
    public string attackParameter;
    #endregion

    #region References
    Animator anim;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    #region Animation Sequence Play Methods
    /// <summary>
    /// Plays use animation.
    /// </summary>
    public void PlayUse()
    {
        if (HasUseParam())
            anim.SetTrigger(useParameter);
    }

    /// <summary>
    /// Plays attack animation.
    /// </summary>
    public void PlayAttack()
    {
        if (HasAttackParam())
        {
            anim.SetTrigger(attackParameter);
        }
    }
    #endregion

    #region Conditional Checks
    /// <summary>
    /// Checks if the animator has a Use parameter.
    /// </summary>
    /// <returns></returns>
    public bool HasUseParam()
    {
        if (string.IsNullOrEmpty(useParameter) == true)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the animator has an Attack parameter.
    /// </summary>
    /// <returns></returns>
    public bool HasAttackParam()
    {
        if (string.IsNullOrEmpty(attackParameter) == true)
        {
            return false;
        }
        return true;
    }
    #endregion
}
