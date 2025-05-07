using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool isUsed = false;
   

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject == null) return;

        DraggableItem droppedItem = droppedObject.GetComponent<DraggableItem>();
        if (droppedItem == null || droppedItem.image.sprite == null) return;

        Transform itemIconTransform = transform.Find("ItemIcon");
        if (itemIconTransform == null) return;

        Image currentImage = itemIconTransform.GetComponent<Image>();
        Sprite currentSprite = currentImage != null ? currentImage.sprite : null;

        // If this slot already has an item, check for combination
        if (isUsed && currentSprite != null)
        {
            Sprite comboResult = ItemCombinationManager.Instance.GetCombinationResult(currentSprite, droppedItem.image.sprite);
            if (comboResult != null)
            {
                Debug.Log("Items Combined!");

                // Set new combined sprite
                currentImage.GetComponentInParent<InventorySlot>().ClearSlot();

                //droppedItem.image = null;
                Debug.Log(droppedItem.parentAfterDrag.name);

                AudioManager.Instance.PlaySound(AudioManager.Instance.combineSound); // reward sound

                // Animate the slot for feedback
                StartCoroutine(AnimateCombination());

                droppedItem.image.sprite = null; // Set new sprite if needed
              
                var playerinventory = FindObjectOfType<PlayerInventory>();
                // add to inventory
                playerinventory.AddItem(comboResult.name,comboResult);

                return;
            }
        }
        if (!isUsed || currentImage.sprite == null)
        {
            var previousparent = droppedItem.parentAfterDrag;
            var newparent = itemIconTransform.parent;
            itemIconTransform.SetParent(previousparent);
            itemIconTransform.position = itemIconTransform.parent.position;
            droppedItem.parentAfterDrag = transform;
            droppedItem.transform.SetParent(newparent);
            droppedItem.transform.position = transform.position;
            isUsed = true;
            previousparent.GetComponent<InventorySlot>().isUsed = false;
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
            Debug.LogWarning("ItemIcon child not found under slot!");
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

    private IEnumerator AnimateCombination()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 enlargedScale = originalScale * 1.2f;

        float duration = 0.2f;
        float elapsed = 0f;

        // Scale up
        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, enlargedScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = enlargedScale;

        // Scale back down
        elapsed = 0f;
        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(enlargedScale, originalScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }
}