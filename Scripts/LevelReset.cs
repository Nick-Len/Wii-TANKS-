using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReset : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> tankL = new List<Vector3>();
    [SerializeField]
    private List<Transform> tanks = new List<Transform>();
    /*private void Awake()
    {
        foreach (Transform tank in tanks)
        {
            tankL.Add(tank.position);
        }
    }*/
    /*public void ResetLevel()
    {
        foreach (Transform tank in tanks)
        {
            tankL.Add(tank.position);
        }
        int i = 0;
        foreach (Transform tank in tanks)
        {
            Debug.Log(tankL.Count);
            tank.position = tankL[i];
            i++;
        }
    }*/
}
