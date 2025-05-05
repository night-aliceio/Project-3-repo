using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlantScript : MonoBehaviour
    {
        public Sprite itemIcon; // Assign in Inspector

        private void OnTriggerEnter(Collider other)
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

            if (playerInventory != null)
            {
                playerInventory.PlantCollected(itemIcon);
                gameObject.SetActive(false);
            }
        }
    }

