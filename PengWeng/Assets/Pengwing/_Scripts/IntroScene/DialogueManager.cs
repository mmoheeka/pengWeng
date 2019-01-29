using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public Queue<string> sentences;
    public TextMeshProUGUI text;
    public bool dialogueEnded;



    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }


    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("starting conversation with " + dialogue.name);



        sentences.Clear();
        dialogueEnded = false;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        text.text = sentences.Dequeue();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        text.text = sentences.Dequeue();

    }

    public void EndDialogue()
    {
        dialogueEnded = true;

        Debug.Log("end of conversation");
    }
}
