using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneIIICinematic : MonoBehaviour
{
    public AudioClip bossMusic;
    public Transform musicManager;
    public Transform player;
    public LookTarget[] lookTargets;

    FirstPersonPlayerController fpControl;
    CameraController camController;

    bool musicTriggered = false;
    bool wasTriggered = false;
    int targetsLookedAt = 0;
    float timePassed = 0;

    // Start is called before the first frame update
    void Start()
    {
        fpControl = player.GetComponent<FirstPersonPlayerController>();
        camController = player.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (wasTriggered || lookTargets.Length == 0)
            return;

        if (other.tag == "Player")
        {
            if (!musicTriggered)
            {
                musicManager.GetComponent<AudioSource>().Stop();
                musicManager.GetComponent<AudioSource>().PlayOneShot(bossMusic, 2.5f);
                musicTriggered = true;
            }

            other.GetComponent<FirstPersonPlayerController>().enabled = false;
            other.GetComponent<CameraController>().enabled = false;

            if (targetsLookedAt < lookTargets.Length)
            {
                for (int i = 0; i < lookTargets.Length; i++)
                {
                    if (lookTargets[i].delay + timePassed <= Time.time && !lookTargets[i].wasLookedAt)
                    {
                        Camera.main.transform.LookAt(lookTargets[i].target.position);
                        lookTargets[i].wasLookedAt = true;

                        timePassed = Time.time;
                        targetsLookedAt += 1;
                    }
                }
            }

            if (targetsLookedAt == lookTargets.Length)
            {
                other.GetComponent<FirstPersonPlayerController>().enabled = true;
                other.GetComponent<CameraController>().enabled = true;
            }
        }
    }
}

[System.Serializable]
public class LookTarget
{
    public Transform target;
    public float delay;
    [HideInInspector] public bool wasLookedAt = false;
}
