using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Renderer mRenderer;
    [SerializeField]
    private Collider cCollider;
    [SerializeField]
    private Collider mCollider;
    [SerializeField]
    private int damage;
    [SerializeField]
    private AudioSource aSource;
    [SerializeField]
    private AudioSource wSource;
    private float speed = 5;
    private Vector3 direction = Vector3.zero;
    private bool bActive;
    private float timer;
    private float lifetime = 8f;
    private int bounces;
    private float bAngle;

    //disable self with timer

    public bool BActive => bActive;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (bActive)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= lifetime)
                DisableSelf();
            transform.position += direction * Time.fixedDeltaTime * speed;
        }
    }
    public void Activate(float spd, Vector3 dir)
    {
        bActive = true;
        mRenderer.enabled = true;
        cCollider.enabled = true;
        mCollider.enabled = true;
        transform.rotation = Quaternion.LookRotation(dir);
        direction = dir;
        speed = spd;
        bounces = 1;
        //shotCounter(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Wall" || bounces == 0)
        {
            if (other.TryGetComponent(out HealthSystem hSystem))
            {
                hSystem.UpdateHealth(damage);
            }
            if (other.TryGetComponent(out AiHealth aSystem))
            {
                aSystem.UpdateHealth(damage);
            }
            aSource.Play();
            DisableSelf();
            Debug.Log(other.gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (bounces > 0)
        {
            if (Vector3.SignedAngle(GetComponent<Transform>().forward, collision.contacts[0].normal, transform.up) > 180)
            {
                bAngle = (Vector3.SignedAngle(GetComponent<Transform>().forward, collision.contacts[0].normal, transform.up) - 90) * 2;
            }
            else
            {
                bAngle = (Vector3.SignedAngle(GetComponent<Transform>().forward, collision.contacts[0].normal, transform.up) + 90) * 2;
            }
            direction = Quaternion.AngleAxis(bAngle, Vector3.up) * transform.forward;
            transform.rotation = Quaternion.LookRotation(direction);
            wSource.Play();
            bounces--;
        }
    }

    public void DisableSelf()
    {
        mRenderer.enabled = false;
        cCollider.enabled = false;
        mCollider.enabled = false;
        bActive = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        timer = 0;
        //shotCounter(1);
        //gameObject.SetActive(false);
    }

    

}
