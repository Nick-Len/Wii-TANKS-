using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    private void OnEnable()
    {
        FollowDelegate.follow += Chase;
    }

    private void OnDisable()
    {
        FollowDelegate.follow -= Chase;
    }

    private void Chase(Transform target)
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
        //Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance <= 10.4f)
        {
            agent.isStopped = true;
        }
    }
}
