using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Image healthImage;

    public void UpdateUI(float ratio)
    {
        healthImage.fillAmount = ratio;
    }
}
