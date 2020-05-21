using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreExplode : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnCoreExplode;

    Requirement winCondition;
    SoundManager soundManager;

    private void Awake()
    {
        if (OnCoreExplode == null)
            OnCoreExplode = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        winCondition = GetComponent<Requirement>();
        soundManager = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (winCondition.RequirementMet())
        {
            OnCoreExplode.Invoke();
            Explode();
        }
    }

    void Explode()
    {
        //soundManager.PlayExplodeSounds();
        Destroy(gameObject);
    }
}
