using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float turnSpeed = 75f;
    [SerializeField]
    private Transform turret;

    private PlayerInput pInput;
    private Vector3 direction;
    private Vector3 hRotation;
    private Vector3 tRotation;
    private int invert;


    void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
    }

    void Update()
    {
        invert = 1;
        direction = transform.forward * pInput.Player.Movement.ReadValue<float>();
        transform.localPosition += direction * speed * Time.deltaTime;
        if (pInput.Player.Movement.ReadValue<float>() < 0f)
            invert = -1;
        hRotation = new Vector3(0, pInput.Player.Turn.ReadValue<float>() * turnSpeed * Time.deltaTime * invert + hRotation.y, 0);
        transform.rotation = Quaternion.Euler(hRotation);
        tRotation = new Vector3(0, pInput.Player.turretTurn.ReadValue<float>() * turnSpeed * Time.deltaTime + tRotation.y, 0);
        turret.localRotation = Quaternion.Euler(tRotation);
    }
}
