using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotatesmooth = 360f;
    [SerializeField]
    private float deadzone = 0.1f;
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
    private Vector3 movement;
    private Vector3 turrot;
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
        if (Mathf.Abs(pInput.Player.Movement.ReadValue<Vector2>().x) > deadzone || Mathf.Abs(pInput.Player.Movement.ReadValue<Vector2>().y) > deadzone)
        {
            movement = pInput.Player.Movement.ReadValue<Vector2>();
            float tmp = movement.x;
            movement.x = movement.y * -1f;
            movement.z = tmp;
            movement.y = 0;
            transform.rotation = Quaternion.LookRotation(movement);
            transform.position += movement * speed * Time.deltaTime;
        }
        if (Mathf.Abs(pInput.Player.Turn.ReadValue<Vector2>().x) > deadzone || Mathf.Abs(pInput.Player.Turn.ReadValue<Vector2>().y) > deadzone)
        {
            turrot = pInput.Player.Turn.ReadValue<Vector2>();
            float tmp = turrot.x;
            turrot.x = turrot.y * -1f;
            turrot.z = tmp;
            turrot.y = 0;
            turret.rotation = Quaternion.LookRotation(turrot);
        }

        if (Mathf.Abs(movement.x) > deadzone || Mathf.Abs(movement.z) > deadzone)
        {
            dist += speed * Time.deltaTime * Mathf.Sqrt((movement.x * movement.x) + (movement.z * movement.z));
            if (dist > dTime)
            {
                trackL.Add(Instantiate(tracks).GetComponent<Transform>());
                trackL[i].rotation = Quaternion.LookRotation(transform.forward);
                trackL[i].localEulerAngles = trackL[i].localEulerAngles + transform.up;
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
