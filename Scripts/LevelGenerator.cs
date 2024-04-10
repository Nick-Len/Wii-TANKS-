using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Transform> obstacle = new List<Transform>();
    [SerializeField]
    private List<Transform> obstacles = new List<Transform>();
    [SerializeField]
    private NavMeshSurface surface;
    [SerializeField]
    private bool bReset = false;
    [SerializeField]
    private int level = 1;

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

    private void Update()
    {
        if (bReset)
        {
            bReset = false;
            worldReset();
        }
    }
    public void worldReset()
    {
        Destroy(obstacles[0].gameObject);
        obstacles.Clear();
        obstacles.Add(Instantiate(obstacle[level-1]).GetComponent<Transform>());
        surface.BuildNavMesh();
    }
}
