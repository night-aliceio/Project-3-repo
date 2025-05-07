using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuitOnEscape : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit(); // This line quits the game in the build version
            Debug.Log("Quit Game.");
        }
    }
}