using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    public static Action newRound = delegate { };
    [SerializeField]
    private List<Transform> obstacle = new List<Transform>();
    [SerializeField]
    private List<Transform> obstacles = new List<Transform>();
    [SerializeField]
    private HealthUI hUI;
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private int level = 1;
    private int bTanks;
    private int greyTanks;
    private int yTanks;
    private int greenTanks;
    private int pTanks;

    public void Start()
    {
        obstacles.Add(Instantiate(obstacle[level - 1]).GetComponent<Transform>());
        foreach (Transform t in obstacles)
        {
            t.gameObject.SetActive(false);
        }
        worldReset();
        Invoke("worldReset", 0.1f);
    }
    private void OnEnable()
    {
        AiHealth.compdeath += aiTracker;
        RestartGame.Restarted += worldReset;
    }

    private void OnDisable()
    {
        AiHealth.compdeath -= aiTracker;
        RestartGame.Restarted -= worldReset;
    }

    public void worldReset()
    {
        Destroy(obstacles[0].gameObject);
        obstacles.Clear();
        obstacles.Add(Instantiate(obstacle[0]).GetComponent<Transform>());
        Invoke("timer", 10f);
        bTanks = obstacle[0].GetComponent<TankTracker>().btank;
        greyTanks = obstacle[0].GetComponent<TankTracker>().greytank;
        yTanks = obstacle[0].GetComponent<TankTracker>().ytank;
        pTanks = obstacle[0].GetComponent<TankTracker>().ptank;
        greenTanks = obstacle[0].GetComponent<TankTracker>().greentank;
        level = 1;
        hUI.health = 3;
        hUI.score = 0;
        hUI.UpdateScore(-1);
        healthImage.fillAmount = 1;
        newRound();
    }

    private void levelProgress()
    {
        Destroy(obstacles[0].gameObject);
        obstacles.Clear();
        obstacles.Add(Instantiate(obstacle[level - 1]).GetComponent<Transform>());
        bTanks = obstacle[level - 1].GetComponent<TankTracker>().btank;
        greyTanks = obstacle[level - 1].GetComponent<TankTracker>().greytank;
        yTanks = obstacle[level - 1].GetComponent<TankTracker>().ytank;
        pTanks = obstacle[level - 1].GetComponent<TankTracker>().ptank;
        greenTanks = obstacle[level - 1].GetComponent<TankTracker>().greentank;
        newRound();
    }

    private void aiTracker(int color)
    {
        if (color == 0)
        {
            bTanks--;
        }
        else if (color == 1)
        {
            greyTanks--;
        }
        else if (color == 2)
        {
            greenTanks--;
        }
        else if (color == 3)
        {
            pTanks--;
        }
        else if (color == 4)
        {
            yTanks--;
        }
        if (bTanks == 0 && greyTanks == 0 && yTanks == 0 && greenTanks == 0 && pTanks == 0)
        {
            level++;
            levelProgress();
            newRound();
        }
    }

    private void timer()
    {
        return;
    }


    public void playerDeath()
    {
        obstacles[0].GetComponent<LevelReset>().ResetLevel();
        newRound();
    }

    public void gameOver()
    {
        obstacles[0].GetComponent<LevelReset>().GameOver();
        newRound();
    }
}
