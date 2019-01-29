using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public Dialogue dialogue;

    private DialogueManager dManager;
    private Animator myAnimator;


    void Awake()
    {
        dManager = FindObjectOfType<DialogueManager>();
        myAnimator = GetComponentInParent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            myAnimator.SetBool("DialogueIntro", true);
            myAnimator.SetBool("DialogueOutro", false);

            TriggerDialogue();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            NextDialogue();
        }


        if (dManager.dialogueEnded)
        {
            myAnimator.SetBool("DialogueOutro", true);
            myAnimator.SetBool("DialogueIntro", false);
        }


    }


    public void TriggerDialogue()
    {

        dManager.StartDialogue(dialogue);

    }

    public void NextDialogue()
    {

        dManager.DisplayNextSentence();

    }


}


