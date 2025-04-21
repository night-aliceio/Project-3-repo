using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory UI")]
    public Transform inventoryPanel;        // UI container with item slots
    public GameObject inventorySlotPrefab;  // A prefab with an Image component

    private List<Sprite> collectedItemIcons = new List<Sprite>();

    public void PlantCollected(Sprite itemIcon)
    {
        collectedItemIcons.Add(itemIcon);
        AddItemToUI(itemIcon);
    }

    void AddItemToUI(Sprite icon)
    {
        GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel);

        // Find the ItemIcon inside the prefab
        Transform itemIconTransform = newSlot.transform.Find("ItemIcon");
        if (itemIconTransform != null)
        {
            Image iconImage = itemIconTransform.GetComponent<Image>();
            if (iconImage != null)
            {
                iconImage.sprite = icon;
            }

            DraggableItem draggable = itemIconTransform.GetComponent<DraggableItem>();
            if (draggable != null)
            {
                draggable.image = iconImage; // Make sure the image field is set!
            }
        }
    }

}
