using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealth : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int maxHealth = 1;
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private List<Collider> colliders;
    [SerializeField]
    private bool resucitate;
    [SerializeField]
    private AiTurret tur;
    [SerializeField]
    private AudioSource aSource;
    public static Action<int> compdeath = delegate { };
    [SerializeField]
    private int color;

    private void Start()
    {
        ResetHealth();
    }
    public void UpdateHealth(int value)
    {
        
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
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }
        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }
        tur.OnDeath();
        compdeath(color);
    }

    public void ResetHealth()
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = true;
        }
        foreach (Collider c in colliders)
        {
            c.enabled = true;
        }
        hp = maxHealth;
        tur.Reset();
    }
}
