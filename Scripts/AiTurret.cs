using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Update()
    {
        bActive = true;
        StartCoroutine(nameof(Aiming));
    }

    private void fire()
    {
        bang.Fire();
    }

    IEnumerator Aiming()
    {
        Vector3 direction;
        Quaternion hRotation;
        while (bActive)
        {
            direction = (target.position - transform.position).normalized;
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
}
