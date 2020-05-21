using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base AI class.
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(AIAnimationController))]
public class AI : MonoBehaviour
{
    #region AI Enums
    /// <summary>
    /// The state of this AI.
    /// </summary>
    public enum CreatureState { Idle, Waiting, Wandering, SeekTarget, Attacking, Dead };

    /// <summary>
    /// The allegiance of this AI relative to the player.
    /// </summary>
    public enum CreatureAllegiance { Neutral, Friendly, Hostile };
    #endregion

    #region AI Attribute Variables
    [Header("AI Attributes")]
    [Tooltip("This AI's default movement speed.")]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] [HideInInspector] public float defaultSpeed;
    [Tooltip("The radius around this AI in which this AI will look for a new spot to move.")]
    public float wanderRadius;
    [Tooltip("How long this AI will wait before moving again, between two floats.")]
    public float[] wanderTimer = new float[2];
    [Tooltip("This AI's detection radius.")]
    public float detectRadius;
    [Tooltip("The distance to the object of interest at which this AI will stop moving.")]
    public float stopDistance;
    [Tooltip("The types of entities this AI will detect, and their target priorities.")]
    public Entity[] entitiesToDetect;

    //The amount of time this AI will wait before wandering again, chosen from the wanderTimer range.
    [HideInInspector] public float wanderTime;

    [Header("Other References")]
    [Tooltip("The layers on which this AI will look for objects of interest.")]
    public LayerMask detectLayers;
    #endregion

    #region Hidden Variables
    //This AI's current speed.
    [HideInInspector]
    [System.Obsolete("This attribute has been deprecated. Use Stats instead.")] public float currentSpeed;
    //This AI's current target.
    [HideInInspector]
    public Entity currentTarget;

    //References.
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public Stats stats;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public AIAnimationController animController;

    //This AI's current state and allegiance.
    [HideInInspector]
    public CreatureState state;
    [HideInInspector]
    public CreatureAllegiance allegiance;

    //Time.time; when this is greater than wanderTimer, the character will begin to wander again.
    [HideInInspector]
    public float timer;
    #endregion

    /// <summary>
    /// Assignments and code called on start.
    /// </summary>
    public virtual void OnStart()
    {
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        animController = GetComponent<AIAnimationController>();
        audioSource = GetComponent<AudioSource>();

        state = CreatureState.Idle;
        allegiance = CreatureAllegiance.Neutral;

        stats.currentMoveSpeed = stats.defaultMoveSpeed;
    }

    
    /// <summary>
    /// Creature scans area within detection radius for objects of interest, then chooses one to focus on.
    /// </summary>
    /// <returns>Returns an Entity.</returns>
    public Entity Detect()
    {
        Entity newEntity;
        Entity targetEntity;
        List<Entity> detectedEntities = new List<Entity>();

        RaycastHit hit = new RaycastHit();
        Ray ray;

        Collider[] hitTargets = Physics.OverlapSphere(transform.position, detectRadius, detectLayers);
        if (hitTargets.Length > 0)
        {
            foreach (Collider target in hitTargets)
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;
                ray = new Ray(transform.position, direction);

                //If this AI has line of sight to the detected target...
                if (Physics.Raycast(ray, out hit, detectRadius) && hit.transform == target.transform)
                {
                    //...run through the list of Entities to detect.
                    for (int i = 0; i < entitiesToDetect.Length; i++)
                    {
                        //If the detected target is an Entity of interest...
                        if (target.GetComponent<Stats>().entityType == entitiesToDetect[i].type)
                        {
                            //...create a new Entity with the target's relevant info and add it to the list of detected Entities.
                            newEntity = new Entity(target.transform, target.GetComponent<Stats>().entityType);
                            newEntity.SetPriority(entitiesToDetect[i].targetPriority);
                            detectedEntities.Add(newEntity);
                        }
                    }
                }
            }

            //Sort the list by order of target priority.
            detectedEntities = detectedEntities.OrderBy(w => w.targetPriority).ToList();
            //To-do: Create a formula to decide whether it's "smart" to pursue the highest-priority entities.
            //Current: Pursue the highest-priority entity.
            if (detectedEntities.Count > 0)
            {
                targetEntity = detectedEntities[0];
                return targetEntity;
            }
            else return null;
        }
        //If there are no detected objects of interest, returns null.
        return null;
    }

    /// <summary>
    /// Gets a random wanderTime from the wanderTimer range.
    /// </summary>
    public float GetWanderTime()
    {
        wanderTime = Random.Range(wanderTimer[0], wanderTimer[1]);

        return wanderTime;
    }
}
