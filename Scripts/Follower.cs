using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;
using UnityEngine.Windows;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private float dTime = 0.3f;
    [SerializeField]
    private GameObject tracks;
    private float dist;
    private List<Transform> trackL = new List<Transform>();
    private int i = 0;
    private int track;
    [SerializeField]
    private AudioSource aSource;
    [SerializeField]
    private AudioSource wSource;
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

        if (agent.velocity != Vector3.zero)
        {

            
                dist += Mathf.Sqrt(agent.velocity.x * agent.velocity.x + agent.velocity.z * agent.velocity.z) * Time.deltaTime;
            if (dist > dTime)
            {
                trackL.Add(Instantiate(tracks).GetComponent<Transform>());
                trackL[i].rotation = Quaternion.LookRotation(transform.forward);
                trackL[i].rotation = Quaternion.Euler(trackL[i].rotation.eulerAngles.x + 90f, trackL[i].rotation.eulerAngles.y, trackL[i].rotation.eulerAngles.z);
                trackL[i].position = transform.position;
                if (track == 1)
                    aSource.Play();
                else
                    wSource.Play();
                track++;
                track = track % 2;
                dist = 0;
                i++;
            }
        }

    }
}
