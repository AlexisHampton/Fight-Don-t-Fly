using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Sprite sprite;
    public List<DialogueSO> dialogue;
    public string[] inbetweenTexts;
    public bool canTalk;

    int index;

    private void Start()
    {
        index = 0;
    }

    public string PickSentence()
    {
        if (index >= inbetweenTexts.Length) index = 0;
        string sentence = inbetweenTexts[index];
        index++;
        return sentence;
    }



}
