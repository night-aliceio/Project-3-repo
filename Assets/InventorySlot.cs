using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        if (draggableItem != null)
        {
            draggableItem.parentAfterDrag = transform;
        }
    }
    
    
    public bool isUsed = false;

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
        isUsed = false;
    }
}

