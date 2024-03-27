using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Renderer mRenderer;
    [SerializeField]
    private Collider cCollider;

    [SerializeField]
    private AudioSource aSource;
    private float speed = 5;
    private Vector3 direction = Vector3.zero;
    private bool bActive;
    private float timer;
    private float lifetime = 5f; 

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
        direction = dir;
        speed = spd;

    }

    private void OnTriggerEnter(Collider other)
    {
        //play sound
        aSource.Play();
        //dissappear
        DisableSelf();
        Debug.Log("Yur mom");
        
    }

    private void DisableSelf()
    {
        mRenderer.enabled = false;
        cCollider.enabled = false;
        bActive = false;
        timer = 0;
        //gameObject.SetActive(false);
    }

    

}
