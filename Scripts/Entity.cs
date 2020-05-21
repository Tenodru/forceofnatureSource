using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity
{
    /// <summary>
    /// This entity's in-game transform.
    /// </summary>
    [HideInInspector] public Transform transform;
    /// <summary>
    /// The type of entity this is. 
    /// </summary>
    [Tooltip("The type of entity.")]
    public EntityType type;
    /// <summary>
    /// The detection priority of this entity set by a detector. Lower number = higher priority.
    /// </summary>
    [Tooltip("The detection priority of this entity. Lower numbers = higher priority.")]
    public int targetPriority;

    public Entity(Transform entityTransform, EntityType entityType)
    {
        transform = entityTransform;
        type = entityType;
    }

    /// <summary>
    /// Sets this entity's priority by the detector.
    /// </summary>
    /// <param name="priority">This entity's target priority.</param>
    public void SetPriority(int priority)
    {
        targetPriority = priority;
    }
}

[System.Serializable]
public enum EntityType
{
    None, Player, RoboWalker, RoboTurret
}
