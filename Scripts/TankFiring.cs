using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(AudioSource))]
public class TankFiring : MonoBehaviour
{
    [SerializeField]
    private AudioSource aSource;
    [SerializeField]
    private int bulletCacheCount = 5;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float firrate = 0.2f;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject bulletPrefab;
    private List<Bullet> bullets = new List<Bullet>();
    private float timer = 0f;

    private bool bActive;
    private bool bTimer;
    private PlayerInput pInput;


    //Called once per session
    private void Start()
    {
        //instantiate all bullets
        for(int i = 0; i < bulletCacheCount; i++)
        {
            bullets.Add(Instantiate(bulletPrefab).GetComponent<Bullet>());
        }

        foreach(Bullet bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }
        
        pInput = new PlayerInput();
        pInput.Enable();
        //bActive = true;
        bActive = (pInput.Player.Shoot.ReadValue<float>() != 0);
        //StartCoroutine(nameof(Firing));
    }
    private void Update()
    {
        bActive = (pInput.Player.Shoot.ReadValue<float>() != 0);
    }
    void FixedUpdate()
    {
        if (bActive)
        {
            if (bTimer)
            {
                timer += Time.fixedDeltaTime;
                if (timer >= firrate)
                {
                    bTimer = false;
                    timer = 0;
                }
            }
            else
            {
                Fire();
                bTimer = true;
            }
        }
    }

    private void Fire()
    {
        foreach (Bullet b in bullets)
        {
            if (!b.BActive)
            {
                b.gameObject.SetActive(true);
                b.transform.position = spawnPoint.position;
                b.Activate(speed, transform.forward);
                aSource.Play();
                bActive = false;
                break;
            }
        }


                /*bullet = Instantiate(bulletPrefab,spawnPoint).GetComponent<Bullet>();
                bullet.transform.localPosition = Vector3.zero; 
                bullet.transform.parent = null;
                bullet.Activate(speed, transform.forward);*/

    }
}
