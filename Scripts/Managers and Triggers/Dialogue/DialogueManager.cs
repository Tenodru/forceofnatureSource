using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The textField that dialogue will appear in.")]
    public TextMeshProUGUI textField;
    [Tooltip("The dialogue box that dialogue will appear in.")]
    public GameObject dialogueBox;

    [Header("Dialogue Lines - Intro")]
    [Tooltip("A sequence of dialogue lines that will be played at game start.")]
    public DialogueSequenceLine[] introSequence;

    [Header("Dialogue Lines - Zone II")]
    [Tooltip("The trigger volume that will trigger the dialogue for Zone II.")]
    public DialogueTrigger zoneTwoTrigger;
    [Tooltip("A sequence of dialogue lines that will be played for Zone II.")]
    public DialogueSequenceLine[] zoneTwoSequence;

    [Header("Dialogue Lines - Zone II and a Half")]
    [Tooltip("The trigger volume that will trigger the dialogue between Zone II and Zone III.")]
    public DialogueTrigger zoneTwoHalfTrigger;
    [Tooltip("A sequence of dialogue lines that will be played between Zone II and Zone III.")]
    public DialogueSequenceLine[] zoneTwoHalfSequence;

    [Header("Dialogue Lines - Zone III")]
    [Tooltip("The trigger volume that will trigger the dialogue for Zone II.")]
    public DialogueTrigger zoneThreeTrigger;
    [Tooltip("A sequence of dialogue lines that will be played for Zone II.")]
    public DialogueSequenceLine[] zoneThreeSequence;

    [Header("Unique Dialogue Lines")]
    [Tooltip("An array of all the unique dialogue lines and their triggers.")]
    public DialogueLine[] uniqueDialogues;

    [Tooltip("The currently playing dialogue sequence.")]
    DialogueSequenceLine[] currentSequence;

    [Tooltip("The time passed since a line was last played.")]
    float timePassed;

    bool playZone2 = false;
    bool playZone2half = false;
    bool playZone3 = false;

    bool dialogueStopped = false;
    bool uniqueTriggered = false;

    int linesPlayed = 0;

    //There will ever only be one DialogueManager.
    public static DialogueManager instance = null;
    public static DialogueManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Another instance of DialogueManager found!");
            Destroy(instance);
            return;
        }
        else
        {
            instance = this;
            Debug.Log("Instance of DialogueManager set.");
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        textField.text = "";
        timePassed = 0;

        currentSequence = introSequence;
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayDialogueSequence(introSequence);

        if (zoneTwoTrigger != null && zoneTwoTrigger.trigger)
        {
            

            playZone2 = true;
            PlayDialogueSequence(zoneTwoSequence);
        }

        if (zoneTwoHalfTrigger != null && zoneTwoHalfTrigger.trigger)
        {
            
            playZone2half = true;
            PlayDialogueSequence(zoneTwoHalfSequence);
        }

        if (zoneThreeTrigger != null && zoneThreeTrigger.trigger)
        {
            
            playZone3 = true;
            PlayDialogueSequence(zoneThreeSequence);
        }
        //PlaySequences();
        CheckLineTriggers();
    }

    public void PlaySequences()
    {
        if (playZone2)
        {
            StopDialogue();
            PlayDialogueSequence(zoneTwoSequence);
        }

        if (playZone2half)
        {
            StopDialogue();
            PlayDialogueSequence(zoneTwoHalfSequence);
        }

        if (playZone3)
        {
            StopDialogue();
            PlayDialogueSequence(zoneThreeSequence);
        }
    }

    /// <summary>
    /// Stop the currently playing dialogue.
    /// </summary>
    public void StopDialogue()
    {
        Array.Clear(currentSequence, 0, currentSequence.Length);
        StopAllCoroutines();
    }

    /// <summary>
    /// Plays the specified dialogue sequence.
    /// </summary>
    public void PlayDialogueSequence(DialogueSequenceLine[] sequence)
    {
        linesPlayed = 0;

        //Intro dialogue sequence.
        if (sequence.Length > 0 && linesPlayed < sequence.Length)
        {
            for (int i = 0; i < sequence.Length; i++)
            {
                //Debug.Log("Time Passed: " + Time.time + ", Time required: " + (sequence[i].delay + timePassed) + ", " + sequence[i].wasPlayed);

                if (sequence[i].delay + timePassed <= Time.time && !sequence[i].wasPlayed)
                {
                    dialogueBox.SetActive(true);
                    textField.text = sequence[i].text;
                    StartCoroutine(ClearLine(sequence[i].stayTime));

                    sequence[i].wasPlayed = true;

                    timePassed = Time.time;
                    linesPlayed += 1;
                }
            }
        }
    }

    /// <summary>
    /// Plays the specified dialogue line.
    /// </summary>
    public void PlayDialogueLine(DialogueLine line)
    {
        dialogueBox.SetActive(true);
        textField.text = line.text;
        StartCoroutine(ClearLine(line.stayTime));
    }

    /// <summary>
    /// Checks if any unique dialogue lines were triggered.
    /// </summary>
    public void CheckLineTriggers()
    {
        if (uniqueDialogues.Length == 0)
        {
            return;
        }

        for (int i = 0; i < uniqueDialogues.Length; i++)
        {
            if (uniqueDialogues[i].trigger.trigger && !uniqueDialogues[i].wasPlayed)
            {
                PlayDialogueLine(uniqueDialogues[i]);
                uniqueDialogues[i].wasPlayed = true;
                Debug.Log("Played line.");
            }
        }
    }

    /// <summary>
    /// Clears the textField after the given time in seconds.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ClearLine(float time)
    {
        yield return new WaitForSeconds(time);

        textField.text = "";
        dialogueBox.SetActive(false);
    }
}

[System.Serializable]
public class DialogueSequenceLine
{
    [Tooltip("The text that will be shown.")]
    [TextArea(5, 10)] public string text;
    [Tooltip("How long after the last line this line will be played.")]
    public float delay;
    [Tooltip("How long this line should stay on the screen before the next line is played.")]
    public float stayTime;

    [HideInInspector] public bool wasPlayed = false;
}

[System.Serializable]
public class DialogueLine
{
    [Tooltip("The dialogue line.")]
    [TextArea(5, 10)] public string text;
    [Tooltip("The trigger that triggers this line.")]
    public DialogueTrigger trigger;
    [Tooltip("How long this line should stay on the screen before the next line is played.")]
    public float stayTime;

    [HideInInspector] public bool wasPlayed = false;
}
