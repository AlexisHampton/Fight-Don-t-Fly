using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public GameObject furniturePanel;
    public GameObject choicePanel;
    public GameObject[] choiceButtons;
    public GameObject continueButton;
    public TextMeshProUGUI furnitureText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image leftImg;
    public Image LleyaImg;

    [Header("Dialogue Stuff")]
    public DialogueSO npcDialogue;
    public float timeUntilNextLetter = 0.03f;
    int index = 0;

    private void Start()
    {
        IsOn(false);
        furniturePanel.SetActive(false);
    }
    public void IsOn(NPC thisNpc)
    {
        //if npc can talk then check, otherwise, feed in first dialogue.
        if (thisNpc.canTalk)
        {
            npcDialogue = thisNpc.dialogue[0];
            leftImg.sprite = thisNpc.sprite;
            thisNpc.dialogue.RemoveAt(0);
            IsOn(true);
            LoadDialogue();
        }
        else
            FurnitureDialogue(thisNpc.PickSentence());
    }

    public void IsOn(bool isOn)
    {
        dialoguePanel.SetActive(isOn);
        continueButton.SetActive(!isOn);
        choicePanel.SetActive(!isOn);
        PlayerMovement.Instance.canMove = !isOn;
        dialogueText.text = " ";
        index = 0;

    }

    public void LoadDialogue()
    {
        if (npcDialogue == null) return;
        //continueButton.SetActive(false);
        Debug.Log(index);
        if (index < npcDialogue.dialogue.Length)
        {
            //split dialogue and read line by line w. index
            dialogueText.text = " ";
            string[] text = npcDialogue.dialogue[index].Split(';');
            nameText.text = text[0];
            dialogueText.text = text[1];
            continueButton.SetActive(true);
        }
        else
        {
            index = 0;
            Debug.Log(index);
            if (npcDialogue.hasChoices)
            {
                choicePanel.SetActive(true);
                for (int i = 0; i < choiceButtons.Length; i++)
                {
                    choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = npcDialogue.choices[i].choiceText;
                }
            }
            else
            {
                Debug.Log("As I told you");
                StoryManager.Instance.IncreasePlace();
                IsOn(false);
            }
            
        }
    }

    public void FurnitureDialogue(Interactable item)
    {
        npcDialogue = null;
        IsOn(false);
        furniturePanel.SetActive(true);
        furnitureText.text = item.dialogue;
    }

    public void FurnitureDialogue(string sentence)
    {
        npcDialogue = null;
        IsOn(false);
        furniturePanel.SetActive(true);
        furnitureText.text = sentence;
    }

    public void Choose(int choiceIndex)
    {
        if (npcDialogue.choices[choiceIndex].isDone)
        {
            IsOn(false);
            StoryManager.Instance.IncreasePlace();
            return;
        }
        npcDialogue = npcDialogue.choices[choiceIndex].choiceDialogue;
        choicePanel.SetActive(false);
        LoadDialogue();
    }

    public void Continue()
    {
        index++;
        //Debug.Log(index);
        LoadDialogue();
        furniturePanel.SetActive(false);
    }

}
