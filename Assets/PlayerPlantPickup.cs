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

    public void PlantCollected(Sprite itemIcon)
    {
        collectedItemIcons.Add(itemIcon);
        AddItemToUI(itemIcon);
    }


    void AddItemToUI(Sprite icon)
    {
        //GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel);
        //Image imageComponent = newSlot.GetComponent<Image>();

        //if (imageComponent != null)
        //{
        //    imageComponent.sprite = icon;
        //}

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

    private Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();

    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName);
    }

    public void AddItem(string itemName, Sprite icon)
    {
        if (!inventory.ContainsKey(itemName))
        {
            inventory.Add(itemName, icon);
            AddItemToUI(icon);
        }
    }
    public bool HasItem(Sprite spriteToCheck)
    {
        foreach (var entry in inventory)
        {
            if (entry.Value != null && entry.Value.name == spriteToCheck.name)
                return true;
        }
        return false;
    }
}
