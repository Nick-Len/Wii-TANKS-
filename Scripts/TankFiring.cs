using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFiring : MonoBehaviour
{
    [SerializeField]
    private int bulletCacheCount = 100;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float firrate = 0.1f;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform target;
    private List<Bullet> bullets = new List<Bullet>();

    private bool bActive;

    

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

        bActive = true;
        StartCoroutine(nameof(Firing));
        StartCoroutine(nameof(Aiming));
    }

    IEnumerator Aiming()
    {
        Vector3 direction;
        while(bActive)
        {
            direction = target.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Firing()
    {
        //Bullet bullet;
        while(bActive)
        {
            yield return new WaitForSeconds(firrate);

            foreach(Bullet b in bullets)
            {
                if(!b.BActive)
                {
                    b.gameObject.SetActive(true);
                    b.transform.position = spawnPoint.position;
                    b.Activate(speed, transform.forward);
                    break;
                }
            }



            /*bullet = Instantiate(bulletPrefab,spawnPoint).GetComponent<Bullet>();
            bullet.transform.localPosition = Vector3.zero; 
            bullet.transform.parent = null;
            bullet.Activate(speed, transform.forward);*/
        }

    }
}
