using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory UI")]
    public Transform inventoryPanel;
    public GameObject inventorySlotPrefab;

    private List<Sprite> collectedItemIcons = new List<Sprite>();

    private Dictionary<string, List<Sprite>> inventory = new Dictionary<string, List<Sprite>>();

    public void PlantCollected(Sprite itemIcon)
    {
        collectedItemIcons.Add(itemIcon);
        AddItem(itemIcon.name, itemIcon);
    }

    public void AddItem(string itemName, Sprite icon)
    {
        if (!inventory.ContainsKey(itemName))
        {
            inventory.Add(itemName, new List<Sprite>());
        }

        // Add the icon to the list of sprites under the itemName
        if (!inventory[itemName].Contains(icon))
        {
            inventory[itemName].Add(icon);
        }
        AddItemToUI(icon);
    }

    //method to remove item

    public bool HasItem(Sprite spriteToCheck)
    {
        foreach (var entry in inventory)
        {
            foreach (var icon in entry.Value)
            {
                // Check if any of the sprites match the given sprite
                if (icon.name == spriteToCheck.name)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName) && inventory[itemName].Count > 0;
    }

    void AddItemToUI(Sprite icon)
    {
        Debug.Log("Checking slots");
        foreach (Transform slotTransform in inventoryPanel)
        {
            InventorySlot slot = slotTransform.GetComponent<InventorySlot>();
            if (slot != null && !slot.isUsed)
            {
                Debug.Log("Slot found: " + slot.gameObject.name);
                slot.SetItem(icon);
                return;
            }
        }

        Debug.LogWarning("No empty inventory slots available!");
    }
}