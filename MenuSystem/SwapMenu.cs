using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwapMenu : MonoBehaviour
{
    public GameObject currentMenu, nextMenu;
    public void NextMenu()
    {
        currentMenu.SetActive(false);
        nextMenu.SetActive(true);
    }
}
