using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool isUsed = false;
    public void OnDrop(PointerEventData eventData)
    {
        {
            GameObject droppedObject = eventData.pointerDrag;
            if (droppedObject == null) return;

            DraggableItem droppedItem = droppedObject.GetComponent<DraggableItem>();
            if (droppedItem == null || droppedItem.image == null) return;

            Transform itemIconTransform = transform.Find("ItemIcon");
            if (itemIconTransform == null) return;

            Image currentImage = itemIconTransform.GetComponent<Image>();

            if (currentImage != null && currentImage.sprite != null)
            {
                // Slot already has something - try COMBINE
                Sprite comboResult = ItemCombinationManager.Instance.GetCombinationResult(currentImage.sprite, droppedItem.image.sprite);

                if (comboResult != null)
                {
                    Debug.Log("✅ Items Combined!");

                    // Update this slot with the new combined sprite
                    currentImage.sprite = comboResult;

                    // Destroy the dragged item
                    Destroy(droppedObject);

                    return;
                }
            }

            // If no combination: just move the item into this slot
            droppedItem.parentAfterDrag = transform;
            droppedItem.transform.SetParent(itemIconTransform.parent);
            droppedItem.transform.position = transform.position;
            isUsed = true;
        }
    }


    public void SetItem(Sprite icon)
    {
        Transform itemIconTransform = transform.Find("ItemIcon");
        if (itemIconTransform != null)
        {
            Debug.Log("Found item transform");
            Image iconImage = itemIconTransform.GetComponent<Image>();
            if (iconImage != null)
            {
                iconImage.sprite = icon;
                iconImage.enabled = true;
            }

            DraggableItem draggable = itemIconTransform.GetComponent<DraggableItem>();
            if (draggable != null)
            {
                draggable.image = iconImage;
                draggable.parentAfterDrag = transform;
            }

            isUsed = true;
        }
        else
        {
            //instantiate item prefab, that has a image and draggableitem component
        }
    }

    public void ClearSlot()
    {
        Transform itemIconTransform = transform.Find("ItemIcon");
        if (itemIconTransform != null)
        {
            Image iconImage = itemIconTransform.GetComponent<Image>();
            if (iconImage != null)
            {
                iconImage.sprite = null;
                iconImage.enabled = false;
            }
        }

        isUsed = false;
    }
}

