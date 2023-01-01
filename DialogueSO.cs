using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kim Seokjin", menuName = "Dialogue")]
public class DialogueSO : ScriptableObject
{

    public Sprite[] sprites;
    public string[] dialogue;
    public bool hasChoices;
    public ChoiceSO[] choices = new ChoiceSO[2];
}
