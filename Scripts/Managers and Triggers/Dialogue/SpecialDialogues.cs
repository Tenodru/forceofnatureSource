using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDialogues : MonoBehaviour
{
    [Header("Dialogue Lines")]
    [Tooltip("All dialogue lines with special triggers.")]
    public DialogueLine[] dialogueLines;

    [Header("References")]
    public DialogueManager dialogueManager;
    public Requirement gateway2Req;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gateway2Req.reqMet_needItem && !dialogueLines[0].wasPlayed)
        {
            dialogueManager.PlayDialogueLine(dialogueLines[0]);
            dialogueLines[0].wasPlayed = true;
        }
    }
}
