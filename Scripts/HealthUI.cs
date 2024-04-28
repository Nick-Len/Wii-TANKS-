using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Image healthImage;
    public int health = 3;
    [SerializeField]
    private LevelGenerator lGen;
    public int score = 0;
    [SerializeField]
    private TextMeshProUGUI counterText;

    private void OnEnable()
    {
        HealthSystem.Die += UpdateUI;
        AiHealth.compdeath += UpdateScore;
    }

    private void OnDisable()
    {
        HealthSystem.Die -= UpdateUI;
        AiHealth.compdeath -= UpdateScore;
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

    public void UpdateScore(int reset)
    {
        score++;
        if (reset == -1)
            score = 0;
        counterText.text = score.ToString();
    }
}
