using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int hp;
    private int maxHealth = 1;
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private List<Collider> colliders;
    [SerializeField]
    private bool resucitate;
    [SerializeField]
    private AudioSource aSource;
    [SerializeField]
    private PlayerMovement pMovement;
    [SerializeField]
    private TankFiring firing;
    public static Action Die = delegate { };

    private void Start()
    {
        ResetHealth();
    }
    public void UpdateHealth(int value)
    {
        Die();
        OnDeath();
    }

    private void Update()
    {
        if (resucitate)
        {
            ResetHealth();
            resucitate = false;
        }
    }
    private void OnDeath()
    {
        aSource.Play();
        /*foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }
        foreach(Collider c in colliders)
        {
            c.enabled = false;
        }*/
    }

    public void ResetHealth()
    {
        hp = maxHealth;
        /*foreach (Renderer r in renderers)
        {
            r.enabled = true;
        }
        foreach (Collider c in colliders)
        {
            c.enabled = true;
        }*/
        
    }
}
