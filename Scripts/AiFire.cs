using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(AudioSource))]
public class AiFire : MonoBehaviour
{
    [SerializeField]
    private AudioSource aSource;
    [SerializeField]
    private int bulletCacheCount = 5;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int bounces = 1;
    private List<Bullet> bullets = new List<Bullet>();



    private void Start()
    {
        for (int i = 0; i < bulletCacheCount; i++)
        {
            bullets.Add(Instantiate(bulletPrefab).GetComponent<Bullet>());
        }

        foreach (Bullet bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }
    }
    public void Fire()
    {
        foreach (Bullet b in bullets)
        {
            if (!b.BActive)
            {
                b.gameObject.SetActive(true);
                b.transform.position = spawnPoint.position;
                b.Activate(speed, transform.forward, bounces);
                aSource.Play();
                break;
            }
        }
    }

    public void disableBullets()
    {
        foreach (Bullet b in bullets)
        {
            b.gameObject.SetActive(false);
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
