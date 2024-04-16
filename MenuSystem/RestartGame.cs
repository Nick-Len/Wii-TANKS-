using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    [SerializeField]
    private GameObject currentMenu, nextMenu;
    public static Action Restarted = delegate { };
    
    public void Restart()
    {
        Debug.Log("Enter code for restart or trigger delegate for restarting");
        Restarted();
        currentMenu.SetActive(false);   
        nextMenu.SetActive(true);
    }
}
