using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SetDefaultButton : MonoBehaviour
{
    [SerializeField]
    private Button defaultButton;
    private void OnEnable()
    {
        defaultButton.Select();
    }
}
