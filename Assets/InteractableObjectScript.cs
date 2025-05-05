using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Required Item")]
    public Sprite requiredSprite;  // The original item sprite

    [Header("Reward Item")]
    public string rewardItemName;
    public Sprite rewardItemSprite;

    [Header("Feedback")]
    [TextArea]
    public string missingItemDialogue = "You don't have the required item.";
    public string[] missingItemDialogueLines;

    [TextArea]
    public string[] successDialogueLines;

    public Dialogue dialogueSystem; //assigned in inspector

    [Header("End Game UI (only for final object)")]
    public GameObject endCanvas;
    public bool isFinalObject = false;

    private bool rewardGiven = false; //tracks when item is given
    private void OnTriggerEnter(Collider other)
    {
        if (rewardGiven) return; //exit if already given
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            if (playerInventory.HasItem(requiredSprite))  // Checks if the player has the required sprite (original or combined)
            {
                // Add reward item
                playerInventory.AddItem(rewardItemName, rewardItemSprite);
                rewardGiven = true;
                Debug.Log($"Gave {rewardItemName} to player.");

                //shows success dialouge
                if (dialogueSystem != null && successDialogueLines.Length > 0 )
                {
                    dialogueSystem.StartDialogue(successDialogueLines);
                }
                if (isFinalObject && endCanvas != null)
                {
                    endCanvas.SetActive(true);
                }
            }
            else
            {
                if (dialogueSystem != null && missingItemDialogueLines.Length > 0)
                {
                    dialogueSystem.StartDialogue(missingItemDialogueLines);
                }
                Debug.Log(missingItemDialogue);
            }
        }
    }
}