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
    private NavMeshSurface surface;
    [SerializeField]
    private HealthUI hUI;
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private int level = 1;
    private int bTanks;
    private int gTanks;
    private int yTanks;
    private int grTanks;
    private int pTanks;

    public void Start()
    {
        obstacles.Add(Instantiate(obstacle[level-1]).GetComponent<Transform>());
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
        /*Destroy(obstacles[0].gameObject);
        obstacles.Clear();
        obstacles.Add(Instantiate(obstacle[0]).GetComponent<Transform>());
        surface.BuildNavMesh();
        bTanks = obstacle[0].GetComponent<TankTracker>().btank;
        gTanks = obstacle[0].GetComponent<TankTracker>().gtank;
        yTanks = obstacle[0].GetComponent<TankTracker>().ytank;
        pTanks = obstacle[0].GetComponent<TankTracker>().ptank;
        grTanks = obstacle[0].GetComponent<TankTracker>().Gtank;
        level = 1;
        hUI.health = 3;
        hUI.score = 0;
        hUI.UpdateScore(-1);
        healthImage.fillAmount = 1;
        newRound();
        surface.BuildNavMesh();*/
        level = 1;
        levelProgress();
    }

    private void levelProgress()
    {
        Destroy(obstacles[0].gameObject);
        obstacles.Clear();
        obstacles.Add(Instantiate(obstacle[level - 1]).GetComponent<Transform>());
        surface.BuildNavMesh();
        bTanks = obstacle[level - 1].GetComponent<TankTracker>().btank;
        gTanks = obstacle[level - 1].GetComponent<TankTracker>().gtank;
        yTanks = obstacle[level - 1].GetComponent<TankTracker>().ytank;
        pTanks = obstacle[level - 1].GetComponent<TankTracker>().ptank;
        grTanks = obstacle[level - 1].GetComponent<TankTracker>().Gtank;
        newRound();
    }

    private void aiTracker(int color)
    {
        if(color == 0)
        {
            bTanks--;
        }
        else if (color == 1)
        {
            gTanks--;
        }
        else if (color == 2)
        {
            grTanks--;
        }
        else if (color == 3)
        {
            pTanks--;
        }
        else if (color == 4)
        {
            yTanks--;
        }
        if(bTanks == 0 && gTanks == 0 && yTanks == 0 && grTanks == 0 && pTanks == 0)
        {
            level++;
            levelProgress();
            newRound();
}
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
