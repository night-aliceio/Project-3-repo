using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit object");
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory != null && itemIcon != null)
        {
            Debug.Log("Adding to inv");
            playerInventory.PlantCollected(itemIcon);
            gameObject.SetActive(false);
        }
    }
}
