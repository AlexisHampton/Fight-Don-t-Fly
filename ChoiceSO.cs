using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kim Taehyung", menuName = "Choice")]
public class ChoiceSO : ScriptableObject
{
    public string choiceText;
    public bool isDone;
    public DialogueSO choiceDialogue;
}
