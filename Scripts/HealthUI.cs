using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Image healthImage;
    public int health = 3;
    [SerializeField]
    private LevelGenerator lGen;

    private void OnEnable()
    {
        HealthSystem.Die += UpdateUI;
    }

    private void OnDisable()
    {
        HealthSystem.Die -= UpdateUI;
    }

    public void UpdateUI()
    {
        health--;
        healthImage.fillAmount = Mathf.Clamp(health / 3f, 0, 1);
        if (health > 0)
            lGen.playerDeath();
        else
            lGen.gameOver();
    }
}
