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
    [SerializeField]
    private AudioSource aSource;
    [SerializeField]
    private AudioSource wSource;
    [SerializeField]
    private GameObject tracks;
    private List<Transform> trackL = new List<Transform>();

    private PlayerInput pInput;
    private Vector3 direction;
    private Vector3 hRotation;
    private Vector3 tRotation;
    private int invert;
    public bool alive;
    private float dist;
    private int track;
    private float dTime = 0.35f;
    private int i = 0;

    void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        alive = true;
        dist = 0;
    }

    void Update()
    {
        if(!alive)
            pInput.Disable();
        invert = 1;
        direction = transform.forward * pInput.Player.Movement.ReadValue<float>();
        transform.localPosition += direction * speed * Time.deltaTime;
        if (pInput.Player.Movement.ReadValue<float>() < 0f)
            invert = -1;
        hRotation = new Vector3(0, pInput.Player.Turn.ReadValue<float>() * turnSpeed * Time.deltaTime * invert + hRotation.y, 0);
        transform.rotation = Quaternion.Euler(hRotation);
        tRotation = new Vector3(0, pInput.Player.turretTurn.ReadValue<float>() * turnSpeed * Time.deltaTime + tRotation.y, 0);
        turret.localRotation = Quaternion.Euler(tRotation);
        if(pInput.Player.Movement.ReadValue<float>() != 0f)
        {
            dist += speed * Time.deltaTime;
            if(dist > dTime)
            {
                trackL.Add(Instantiate(tracks).GetComponent<Transform>());
                trackL[i].rotation = Quaternion.Euler(hRotation.x + 90, hRotation.y, hRotation.z);
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
    public void Enable()
    {
        alive = true;
        pInput.Enable();
    }
}
