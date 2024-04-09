using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface surface;
    [SerializeField]
    private GameObject obstacle;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject gTank;
    [SerializeField]
    private List<Vector3> oPositions;
    [SerializeField]
    private Vector3 pPosition;
    [SerializeField]
    private List<Vector3> gPositions;
    private List<Transform> obstacles = new List<Transform>();
    private List<Transform> gTanks = new List<Transform>();
    private List<Transform> pTanks = new List<Transform>();
    [SerializeField]
    private bool bReset = false;

    public void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (bReset)
        {
            Reset();
        }
    }
    public void Reset()
    {
        //Clear all tanks
        foreach (Transform o in obstacles)
        {
            obstacles.Remove(o);
            Destroy(o.gameObject);
        }
        foreach (Transform g in gTanks)
        {
            obstacles.Remove(g);
            Destroy(g.gameObject);
        }
        foreach (Transform p in pTanks)
        {
            obstacles.Remove(p);
            Destroy(p.gameObject);
        }

        //make and place obstacles
        for (int i = 0; i < oPositions.Count; i++)
        {
            obstacles.Add(Instantiate(obstacle).GetComponent<Transform>());
        }
        int loc = 0;
        foreach (Transform o in obstacles)
        {
            Vector3 curPos = Vector3.Scale(oPositions[loc], new Vector3(2.6f,2.6f,2.6f));
            o.transform.localPosition = curPos;
            loc++;
        }
        //bake navmesh of new environment
        surface.BuildNavMesh();

        //make and place green tanks
        for (int i = 0; i < gPositions.Count; i++)
        {
            gTanks.Add(Instantiate(gTank).GetComponent<Transform>());
        }
        loc = 0;
        foreach (Transform g in gTanks)
        {
            Vector3 curPos = Vector3.Scale(gPositions[loc], new Vector3(2.6f, 2.6f, 2.6f));
            g.transform.localPosition = curPos;
            loc++;
        }

        //make and place player tank
        gTanks.Add(Instantiate(gTank).GetComponent<Transform>());
        foreach (Transform p in pTanks)
        {
            Vector3 curPos = Vector3.Scale(pPosition, new Vector3(2.6f, 2.6f, 2.6f));
            p.transform.localPosition = curPos;
        }
    }
}
