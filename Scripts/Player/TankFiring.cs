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
    private float speed = 6;
    [SerializeField]
    private float firrate = 0.5f;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject bulletPrefab;
    private List<Bullet> bullets = new List<Bullet>();
    private float timer = 0f;

    private bool bActive;
    public bool alive;
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
        timer = firrate + 1;
        //StartCoroutine(nameof(Firing));
    }

    private void OnEnable()
    {
        alive = true;
    }

    void Update()
    {
        if(!alive)
            pInput.Disable();
        bActive = (pInput.Player.Shoot.ReadValue<float>() != 0);
        timer += Time.deltaTime;
        if (bActive)
        {
            if (timer >= firrate)
            {
                Fire();
                timer = 0;
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
    }

    private void OnDestroy()
    {
        foreach (Bullet b in bullets)
        {
            if (b != null)
                Destroy(b.gameObject);
        }
    }
}
