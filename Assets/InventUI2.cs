using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventUI2 : MonoBehaviour
{
  
    private TextMeshProUGUI plantText;

    // Start is called before the first frame update
    void Start()
    {
        plantText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDiamondText(PlayerInventory playerInventory)
    {
        
    }
}

