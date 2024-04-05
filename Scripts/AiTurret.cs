using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

public class AiTurret : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 75;
    [SerializeField]
    private bool bActive;
    [SerializeField]
    private AiFire bang;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private float firrate;

    private void Start()
    {
        bActive = true;
        if (bActive)
        {
            StartCoroutine(nameof(Aiming));
            StartCoroutine(nameof(Firing));
        }
    }

    /*void Update()
    {
        if (bActive)
        {
            StartCoroutine(nameof(Aiming));
            StartCoroutine(nameof(Firing));
        }
    }*/

    private void fire()
    {
        bang.Fire();
    }

    IEnumerator Aiming()
    {
        Vector3 direction;
        Quaternion hRotation;

        while(bActive)
        {
            direction = (target.position - transform.position).normalized;
            direction.y = 0;
            /*hRotation = new Vector3(Mathf.Clamp(direction.x, transform.rotation.x - (speed * Time.deltaTime), transform.rotation.x + (speed * Time.deltaTime)),
                0,
                Mathf.Clamp(direction.z, transform.rotation.z - (speed * Time.deltaTime), transform.rotation.z + (speed * Time.deltaTime)));
            transform.rotation = Quaternion.LookRotation(hRotation);*/
            hRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, hRotation, Time.deltaTime * speed);
            //transform.rotation = Quaternion.LookRotation(direction);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Firing()
    {
        RaycastHit hit;
        while (bActive)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, agent.remainingDistance, 1))
            {
                if (!Physics.Raycast(transform.position, transform.forward, out hit, agent.remainingDistance - 1.5f, 1))
                {
                    fire();
                }
            }
            yield return new WaitForSeconds(firrate);
        }
    }
    public void OnDeath()
    {
        bActive = false;
    }
    public void Reset()
    {
        bActive = true;
    }
}
