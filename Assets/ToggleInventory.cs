using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using Cinemachine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI;
    public KeyCode toggleKey = KeyCode.I;

    public GameObject player;
    private FirstPersonController fpc;
    private PlayerInput playerInput;
    private CinemachineInputProvider cinemachineInput;

    private bool isInventoryOpen = false;

    void Start()
    {
        // Get components once at the start
        if (player != null)
        {
            fpc = player.GetComponent<FirstPersonController>();
            playerInput = player.GetComponent<PlayerInput>();
            cinemachineInput = FindObjectOfType<CinemachineInputProvider>();
        }

        CloseInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (isInventoryOpen)
                CloseInventory();
            else
                OpenInventory();
        }
    }

    void OpenInventory()
    {
        isInventoryOpen = true;
        inventoryUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (fpc != null) fpc.enabled = false;
        if (cinemachineInput != null) cinemachineInput.enabled = false;
    }

    void CloseInventory()
    {
        isInventoryOpen = false;
        inventoryUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (fpc != null) fpc.enabled = true;
        if (cinemachineInput != null) cinemachineInput.enabled = true;
    }
}
