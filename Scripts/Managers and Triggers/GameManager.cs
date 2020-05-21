using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject factorySmoke;
    [SerializeField] GameObject factory;
    [SerializeField] GameObject factoryDestroyed;
    [SerializeField] GameObject endCutscenePos;
    [SerializeField] GameObject endCutsceneTarget;
    [SerializeField] GameObject victoryText;
    [SerializeField] FadeImage fadeImage;

    [SerializeField] AudioClip mainMusic;
    [SerializeField] Transform musicManager;
    [SerializeField] Transform player;
    [SerializeField] WaveSpawner waveSpawner;

    [Header("Events")]
    public UnityEvent OnLevelStart;

    FirstPersonPlayerController fpControl;
    CameraController camController;

    bool isFading;
    bool wasTriggered = false;
    int targetsLookedAt = 0;
    float timePassed = 0;

    private void Awake()
    {
        if (OnLevelStart == null)
            OnLevelStart = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnLevelStart.Invoke();
        victoryText.SetActive(false);

        fpControl = player.GetComponent<FirstPersonPlayerController>();
        camController = player.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLevelWin()
    {
        player.GetComponent<FirstPersonPlayerController>().enabled = false;
        player.GetComponent<CameraController>().enabled = false;
        player.GetComponent<Health>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;

        waveSpawner.enabled = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            //enemy.GetComponent<EnemyHealth>().Die();
            Destroy(enemy);
        }

        musicManager.GetComponent<AudioSource>().Stop();
        musicManager.GetComponent<AudioSource>().PlayOneShot(mainMusic);

        fadeImage.FadeIn();
        StartCoroutine(ShowEndCutscene(3));

        /*
        if (other.tag == "Player")
        {
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
        }*/
    }

    IEnumerator ShowEndCutscene(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(factorySmoke);
        Destroy(factory);

        Instantiate(factoryDestroyed);

        player.transform.position = endCutscenePos.transform.position;
        Camera.main.transform.LookAt(endCutsceneTarget.transform.position);
        fadeImage.FadeOut();

        victoryText.SetActive(true);

        ScreenFadeOut(8);
    }

    IEnumerator ScreenFadeOut(float time)
    {
        yield return new WaitForSeconds(time);

        fadeImage.FadeIn();
    }
}
