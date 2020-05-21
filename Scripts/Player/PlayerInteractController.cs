using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    [Header("Player Interaction Attributes")]
    [Tooltip("The radius within which the player can interact with objects.")]
    [SerializeField] float interactRadius = 5.0f;
    [Tooltip("The radius within which the player will pickup objects.")]
    [SerializeField] float pickupRadius = 1.0f;

    [Header("References")]
    [Space]
    [SerializeField] LayerMask interactLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //If the left mouse button was pressed and a raycast hits something....
        if (Input.GetButtonDown("Fire1") && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactRadius))
        {
            //...and the detected object has an Interactable component...
            if (hit.transform.GetComponent<Interactable>() != null)
            {
                //Debug.Log("Detected interactable: " + hit.transform.name);
                hit.transform.GetComponent<Interactable>().OnClicked();
            }
        }

        Collider[] detectedPickups = Physics.OverlapSphere(transform.position, pickupRadius, interactLayers);

        if (detectedPickups.Length > 0)
        {
            foreach (Collider detectedObject in detectedPickups)
            {
                if (detectedObject.GetComponent<Pickup>() != null)
                {
                    //Debug.Log("Detected " + detectedObject.name);
                    detectedObject.GetComponent<Pickup>().PickupItem();

                    //If the item has a pickup sound, play the pickup sound.
                    if (detectedObject.GetComponent<Pickup>().pickupSound != null)
                    {
                        GetComponent<AudioSource>().PlayOneShot(detectedObject.GetComponent<Pickup>().pickupSound);
                    }
                }
            }
        }

        Collider[] detectedInteractables = Physics.OverlapSphere(transform.position, interactRadius, interactLayers);

        if (detectedInteractables.Length > 0)
        {
            foreach (Collider detectedObject in detectedInteractables)
            {
                //Interact with detectedObject.
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TriggerVolume>() != null)
        {
            //Has TriggerVolume component.
            other.GetComponent<TriggerVolume>().TriggerEntered(true);
        }
    }

    /// <summary>
    /// Draws spheres around the player representing their interact and pickup areas.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
