using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType { NPC, ITEM}
public class Interactable : MonoBehaviour
{
    public InteractableType interactableType;
    public string dialogue;

    public void Dialogue()
    {
        DialogueManager.Instance.FurnitureDialogue(this);
    }
}
