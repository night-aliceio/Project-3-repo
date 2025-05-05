using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject2 : MonoBehaviour
{
    [Header("Required Items (both required)")]
    public Sprite requiredSprite1;
    public Sprite requiredSprite2;

    [Header("Reward Item")]
    public string rewardItemName;
    public Sprite rewardItemSprite;

    [Header("Feedback")]
    [TextArea]
    public string missingItemDialogue = "You don't have the required items.";
    public string[] missingItemDialogueLines;

    [TextArea]
    public string[] successDialogueLines;

    [Header("Dialogue and End Canvas")]
    public Dialogue dialogueSystem; // Assign in Inspector
    public bool isFinalObject = false; // Toggle in Inspector
    public GameObject endCanvas;       // Assign in Inspector (optional)

    private bool rewardGiven = false;

    private void Start()
    {
        if (endCanvas != null)
            endCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (rewardGiven) return;

        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory == null) return;

        bool hasBothItems = playerInventory.HasItem(requiredSprite1) && playerInventory.HasItem(requiredSprite2);

        if (hasBothItems)
        {
            playerInventory.AddItem(rewardItemName, rewardItemSprite);
            rewardGiven = true;

            if (dialogueSystem != null && successDialogueLines.Length > 0)
                dialogueSystem.StartDialogue(successDialogueLines);

            if (isFinalObject && endCanvas != null)
                endCanvas.SetActive(true);

            Debug.Log($"Gave {rewardItemName} to player.");
        }
        else
        {
            if (dialogueSystem != null && missingItemDialogueLines.Length > 0)
                dialogueSystem.StartDialogue(missingItemDialogueLines);

            Debug.Log(missingItemDialogue);
        }
    }
}