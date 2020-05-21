using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An openable/closable obstacle.
/// </summary>
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [Header("Door Attributes")]
    [SerializeField] Requirement openRequirement;
    [SerializeField] CloseRequirement closeRequirement;

    [HideInInspector] public Animator anim;

    bool wasOpened = false;
    bool wasClosed = false;

    /// <summary>
    /// Opens this door.
    /// </summary>
    public virtual void Open()
    {
        if (wasOpened)
        {
            return;
        }
        Debug.Log(name + " was opened.");
        wasOpened = true;
    }

    /// <summary>
    /// Closes this door.
    /// </summary>
    public virtual void Close()
    {
        if (wasClosed)
        {
            return;
        }
        Debug.Log(name + " was closed.");
        wasClosed = true;
    }

    /// <summary>
    /// Gets this Door's open requirement, if there is one.
    /// </summary>
    /// <returns></returns>
    public Requirement GetOpenRequirement()
    {
        if (openRequirement == null)
        {
            Debug.Log("This door doesn't have an open requirement!");
            return null;
        }
        return openRequirement;
    }

    /// <summary>
    /// Gets this Door's close requirement, if there is one.
    /// </summary>
    /// <returns></returns>
    public CloseRequirement GetCloseRequirement()
    {
        if (closeRequirement == null)
        {
            Debug.Log("This door doesn't have a close requirement!");
            return null;
        }
        return closeRequirement;
    }
}
