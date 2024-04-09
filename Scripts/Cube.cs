using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private Vector3 origPosition;
    private Quaternion origRotation;
    private Rigidbody origRigidBody;

    private void Start()
    {
        origPosition = transform.position;
        origRotation = transform.rotation;
        origRigidBody = GetComponent<Rigidbody>();
    }


    private void Reset()
    {
        origRigidBody.velocity = Vector3.zero;
        origRigidBody.angularVelocity = Vector3.zero;
        transform.position = origPosition;
        transform.rotation = origRotation;
    }
}
