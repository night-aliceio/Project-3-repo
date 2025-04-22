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
        GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel);
        Image imageComponent = newSlot.GetComponent<Image>();

        if (imageComponent != null)
        {
            imageComponent.sprite = icon;
        }
    
        foreach (Transform slotTransform in inventoryPanel)
        {
            InventorySlot slot = slotTransform.GetComponent<InventorySlot>();
            if (slot != null && !slot.isUsed)
            {
                slot.SetItem(icon);
                return;
            }
        }

        Debug.LogWarning("No empty inventory slots available!");
    }

   
    
    public int NumberOfPlants { get; private set; }

    public UnityEvent<PlayerInventory> OnDiamondCollected;

    public void DiamondCollected()
    {
        NumberOfPlants++;
        OnDiamondCollected.Invoke(this);
    }
}

