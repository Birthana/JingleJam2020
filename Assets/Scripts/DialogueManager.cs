using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TextMeshProUGUI dialogueText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DisplayDialogue("Test, Test, Test, Test, Test");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }
}
