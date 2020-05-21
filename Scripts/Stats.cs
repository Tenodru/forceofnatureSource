using AscentUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Broad class for general stats management.
/// </summary>
public class Stats : MonoBehaviour
{
    [Header("Character Stats: General")]
    [Tooltip("The name of this entity, if it has one.")]
    public string entityName;
    [Tooltip("The type of entity this is.")]
    public EntityType entityType;

    [Header("Character Stats: Health")]
    [Tooltip("The default health of this entity.")]
    public int defaultHealth;
    [Tooltip("The default armor of this entity.")]
    public int defaultArmor;
    [Tooltip("The default barrier of this entity.")]
    public int defaultBarrier;

    [HideInInspector] public int currentHealth;
    [HideInInspector] public int currentArmor;
    [HideInInspector] public int currentBarrier;


    [Header("Character Stats: Combat")]
    [Tooltip("The default attack damage of this entity.")]
    public int defaultDamage;
    [Tooltip("The default attack speed of this entity; higher numbers = faster attack speed.")]
    public float defaultAttackSpeed;
    [Tooltip("The time delay between attack trigger and damage dealt.")]
    public float defaultAttackDelay;

    [HideInInspector] public int currentDamage;
    [HideInInspector] public float currentAttackSpeed;
    [HideInInspector] public float currentAttackDelay;


    [Header("Character Stats: Movement")]
    [Tooltip("The default movement speed of this entity.")]
    public float defaultMoveSpeed;
    [Tooltip("The default jump force of this entity.")]
    public float defaultJumpForce;
    [Tooltip("The speed of this entity when in godmode.")]
    public float godModeSpeed;

    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentJumpForce;


    [Header("Events")]
    public UnityEvent OnHealthChanged;
    public UnityEvent OnDamageChanged;


    StatsDisplay statsDisplay;

    public void Awake()
    {
        if (OnHealthChanged == null)
            OnHealthChanged = new UnityEvent();
        if (OnDamageChanged == null)
            OnDamageChanged = new UnityEvent();
    }

    public void Start()
    {
        statsDisplay = GetComponent<StatsDisplay>();
    }

    /// <summary>
    /// Changes current health by the specified amount and calls the OnHealthChanged event.
    /// </summary>
    /// <param name="amount">The amount to change current health by.</param>
    public virtual void ChangeHealth(int amount)
    {
        currentHealth += amount;
        OnHealthChanged.Invoke();
        if (statsDisplay != null)
            statsDisplay.ChangeHealthDisplay(amount);
    }

    /// <summary>
    /// Changes current damage by the specified amount and calls the OnDamageChanged event.
    /// </summary>
    /// <param name="amount">The amount to change current damage by.</param>
    public virtual void ChangeDamage(int amount)
    {
        currentDamage += amount;
        OnDamageChanged.Invoke();
    }
}
