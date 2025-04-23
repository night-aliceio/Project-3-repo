using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Required Item")]
    public Sprite requiredSprite;  // Assign the required sprite in the Inspector

    [Header("Reward Item")]
    public string rewardItemName;
    public Sprite rewardItemSprite;

    [Header("Feedback")]
    [TextArea]
    public string missingItemDialogue = "You don't have the required item.";

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            if (playerInventory.HasItem(requiredSprite))
            {
                // Add reward item
                playerInventory.AddItem(rewardItemName, rewardItemSprite);
                Debug.Log($"Gave {rewardItemName} to player.");
            }
            else
            {
                // Show dialogue or message
                Debug.Log(missingItemDialogue);
            }

        }
    }
}