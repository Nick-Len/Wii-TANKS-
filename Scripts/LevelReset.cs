using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReset : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> tankL = new List<Vector3>();
    [SerializeField]
    private List<Quaternion> tankR = new List<Quaternion>();
    [SerializeField]
    private List<Transform> tanks;// = new List<Transform>();
    private Vector3 pStart;
    private Quaternion pStartR;
    [SerializeField]
    private GameObject player;
    private void Awake()
    {
        
        foreach (Transform tank in tanks)
        {
            tankL.Add(tank.position);
            tankR.Add(tank.rotation);
        }
        pStart = player.transform.position;
        pStartR = player.transform.rotation;
    }
    public void ResetLevel()
    {
        int i = 0;
        foreach (Transform tank in tanks)
        {
            tank.GetComponentInChildren<AiFire>().disableBullets();
            if (tank.GetComponent<Collider>().enabled)
            {
                tank.position = tankL[i];
                tank.rotation = tankR[i];
            }
            i++;
        }
        player.transform.position = pStart;
        player.transform.rotation = pStartR;
        player.GetComponentInChildren<TankFiring>().disableBullets();
        Invoke("timer", 0.1f);
        player.GetComponentInChildren<HealthSystem>().ResetHealth();
    }

    private void timer()
    {
        return;
    }

    public void GameOver()
    {
        Destroy(player);
    }
}
