using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int maxHealth = 3;
    [SerializeField]
    private HealthUI hUI;
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

    private void Start()
    {
        ResetHealth();
    }
    public void UpdateHealth(int value)
    {
        hp -= value;
        hp = Mathf.Clamp(hp, 0, maxHealth);

        hUI.UpdateUI(hp / (float)maxHealth);
        if(hp == 0)
        {
            OnDeath();
        }
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
        pMovement.alive = false;
        firing.alive = false;
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }
        foreach(Collider c in colliders)
        {
            c.enabled = false;
        }
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
    }
}
