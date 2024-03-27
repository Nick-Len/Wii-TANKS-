using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public bool bBoom;
    [SerializeField]
    private List<Rigidbody> rBodies;
    [SerializeField]
    private float force = 5;
    [SerializeField]
    private float radius = 5;
    private List<Vector3> positions;
    private List<Quaternion> rotations;


    private void Update()
    {
        if (bBoom)
        {
            bBoom = false;
            Boom();
        }
    }
    private void Awake()
    {
        positions = new List<Vector3>();
        rotations = new List<Quaternion>(); 
        foreach(Rigidbody piece in rBodies)
        {
            positions.Add(piece.position);
            rotations.Add(piece.rotation);
            piece.isKinematic = true;
            piece.useGravity = false;
        }
    }
    public void Boom()
    {
        ResetPieces();
        Invoke("Execute", 0.1f);
    }
    public void Execute()
    {
        foreach (Rigidbody piece in rBodies)
        {
            piece.isKinematic = false;
            piece.useGravity = true;
            piece.AddExplosionForce(force, transform.position, radius);
        }
    }
    private void ResetPieces()
    {
        for(int i = 0; i < rBodies.Count; i++)
        {
            rBodies[i].isKinematic = true;
            rBodies[i].useGravity = false; 
            rBodies[i].transform.position = positions[i];
            rBodies[i].transform.rotation = rotations[i];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Explode>().Boom();
    }
}
