using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Stats))]
/// <summary>
/// General health system handler.
/// </summary>
public class Health : MonoBehaviour
{
    #region Health Attributes (Deprecated)
    [Header("Health Attributes")]
    [Tooltip("This character's default health.")]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] [HideInInspector] public int defaultHealth;
    [Tooltip("This character's default defense stat.")]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] [HideInInspector] public int defaultArmor;
    [Tooltip("This character's default barrier amount.")]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] [HideInInspector] public int defaultBarrier;
    #endregion

    #region Hidden Health Attributes (Deprecated)
    [HideInInspector]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] public int currentHealth;
    [HideInInspector]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] public int currentArmor;
    [HideInInspector]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] public int currentBarrier;
    #endregion

    #region Events
    [Header("Events")]
    public UnityEvent OnTookDamage;
    public UnityEvent OnCharacterKilled;
    #endregion

    #region Other Hidden Variables
    [HideInInspector]
    public Stats stats;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public SoundManager soundManager;
    [HideInInspector]
    public bool isDead = false;
    #endregion

    private void Awake()
    {
        stats = GetComponent<Stats>();

        stats.currentHealth = stats.defaultHealth;
        stats.currentArmor = stats.defaultArmor;
        stats.currentBarrier = stats.defaultBarrier;

        if (OnTookDamage == null)
            OnTookDamage = new UnityEvent();
        if (OnCharacterKilled == null)
            OnCharacterKilled = new UnityEvent();

        soundManager = GetComponent<SoundManager>();

        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Deals damage to this character.
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage)
    {
        if (!isDead)
        {
            //Play hurt sequence via animator.
            //Update this character's health display.

            OnTookDamage.Invoke();
            Debug.Log(name + " took " + damage + " damage!");

            soundManager.PlayHurtSound(0.5f);

            stats.ChangeHealth(-damage);

            if (stats.currentHealth <= 0)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// Kills this character.
    /// </summary>
    public virtual void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        //Play death sequence via animator.
        //Update any stats and displays that change on character death.

        OnCharacterKilled.Invoke();
        Debug.Log(name + " died!");

        //Disables this character's colliders and physics.
        if (GetComponent<Rigidbody>() == null)
        {
            Debug.Log("No Rigidbody detected on killed character.");
            return;
        }
        else
        {
            //Disable collision with things that this shouldn't collide with. QoL.
        }
    }
}
