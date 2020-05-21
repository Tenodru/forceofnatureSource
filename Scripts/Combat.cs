using UnityEngine;

/// <summary>
/// A broad combat system handler.
/// </summary>
[RequireComponent(typeof(Stats))]
public class Combat : MonoBehaviour
{
    #region Combat Attributes (Deprecated)
    [Header("Damage Attributes")]
    [Tooltip("This character's default damage.")]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] [HideInInspector] public int defaultDamage;
    [Tooltip("How many times this character attacks per second. Higher numbers = faster attack speed.")]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] [HideInInspector] public float defaultAttackSpeed;
    #endregion

    #region Hidden Combat Attributes (Deprecated)
    /// <summary>
    /// This character's current damage.
    /// </summary>
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")]  [HideInInspector] public int currentDamage;
    /// <summary>
    /// This character's current attack speed.
    /// </summary>
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")]  [HideInInspector] public float currentAttackSpeed;
    #endregion

    #region Hidden Combat Variables
    /// <summary>
    /// This character's next attack time.
    /// </summary>
    [HideInInspector] public float nextAttackTime = 0;
    #endregion

    #region Other References
    [HideInInspector] public Stats stats;
    #endregion

    /// <summary>
    /// This character will attack a target when called.
    /// </summary>
    public virtual void Attack()
    {
        //Attack.
        Debug.Log(gameObject.name + " attacked!");
    }

    /// <summary>
    /// Assignments and code called on start.
    /// </summary>
    public virtual void OnStart()
    {
        stats = GetComponent<Stats>();

        stats.currentDamage = stats.defaultDamage;
        stats.currentAttackSpeed = stats.defaultAttackSpeed;
        stats.currentAttackDelay = stats.defaultAttackDelay;
    }

    [System.Obsolete("Default damage can be accessed directly.")]
    public int GetDefaultDamage()
    {
        return stats.defaultDamage;
    }

    [System.Obsolete("Default attack speed can be accessed directly.")]
    public float GetDefaultAttackSpeed()
    {
        return stats.defaultAttackSpeed;
    }
}
