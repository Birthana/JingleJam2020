using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TextMeshPro playerDialogueBox;
    private TextMeshPro otherDialogueBox;
    public TextAsset dialogueFile;
    private List<string> dialogueLines = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GetDialogue(dialogueFile);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetDialogue(TextAsset file)
    {
        string[] lines = file.text.Split(new char[] { '\n' });
        foreach (string line in lines)
        {
            dialogueLines.Add(line);
        }
    }

    public void DisplayDialogue(TextMeshPro npcDialogueBox)
    {
        otherDialogueBox = npcDialogueBox;
        StartCoroutine(Displaying(NextLine()));
    }

    public string NextLine()
    {
        string nextLine = dialogueLines[0];
        dialogueLines.RemoveAt(0);
        return nextLine;
    }

    public void DisplayNext()
    {
        if (dialogueLines.Count == 0)
        {
            otherDialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
            GameManager.instance.StopTalking();
        }
        else
        {
            StartCoroutine(Displaying(NextLine()));
        }
    }

    IEnumerator Displaying(string dialogue)
    {
        string[] split = dialogue.Split(new char[] { ':'});
        int.TryParse(split[0], out int characterNumber);
        if (characterNumber == 0)
        {
            playerDialogueBox.gameObject.transform.parent.gameObject.SetActive(true);
            otherDialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
            playerDialogueBox.text = "";
            foreach (char letter in split[1])
            {
                playerDialogueBox.text += letter;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            playerDialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
            otherDialogueBox.gameObject.transform.parent.gameObject.SetActive(true);
            otherDialogueBox.text = "";
            foreach (char letter in split[1])
            {
                otherDialogueBox.text += letter;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
