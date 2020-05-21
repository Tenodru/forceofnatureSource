using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AscentUI;
using System;

[RequireComponent(typeof(Stats))]
public class StatsDisplay : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The orientation of this Display.")]
    public BarOrientation orientation;

    [Header("UI/Display References")]
    [Tooltip("This character's health display element.")]
    public DisplayElementBar healthDisplay;

    float percentage;
    float barChange;

    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    /// <summary>
    /// Updates this character's health display.
    /// </summary>
    /// <param name="amount"></param>
    public virtual void ChangeHealthDisplay(int amount)
    {
        percentage = (float) amount / (float) stats.defaultHealth;

        if (orientation == BarOrientation.Vertical)
        {
            barChange = percentage * healthDisplay.barDefaultHeightSize;
            healthDisplay.UpdateBar(BarAttribute.Height, barChange, stats.currentHealth);
        }
        if (orientation == BarOrientation.Horizontal)
        {
            barChange = percentage * healthDisplay.barDefaultWidthSize;
            healthDisplay.UpdateBar(BarAttribute.Width, barChange, stats.currentHealth);
        }
    }

    /// <summary>
    /// Code called on start.
    /// </summary>
    public virtual void OnStart()
    {
        stats = GetComponent<Stats>();
        healthDisplay = new DisplayElementBar(healthDisplay.bar, healthDisplay.label, healthDisplay.displayText, healthDisplay.background);
    }
}

