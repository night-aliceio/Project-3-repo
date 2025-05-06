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

    [Header("Audio")]
    public AudioClip collectSound;
    private AudioSource audioSource;

    private List<Sprite> collectedItemIcons = new List<Sprite>();
    private Dictionary<string, List<Sprite>> inventory = new Dictionary<string, List<Sprite>>();

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

        if (!inventory[itemName].Contains(icon))
        {
            inventory[itemName].Add(icon);
        }

        AddItemToUI(icon);

        // Play collection sound
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }

    public bool HasItem(Sprite spriteToCheck)
    {
        foreach (var entry in inventory)
        {
            foreach (var icon in entry.Value)
            {
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