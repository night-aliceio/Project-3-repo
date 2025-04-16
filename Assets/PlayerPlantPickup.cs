using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

public class PlayerPlantPickup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera cam;
    [SerializeField] InventoryUIManager inventoryUIManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo, 3))
            {
                Object_Pickup item = hitinfo.collider.gameObject.GetComponent<Object_Pickup>();
                if (item != null)
                {
                  
                }
            }
        }
    }
}
