using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private HealthUI hUI;

    private void Start()
    {
        ResetHealth();
    }
    public void UpdateHealth(int value)
    {
        hp -= value;
        hp = Mathf.Clamp(hp, 0, maxHealth); 

        hUI.UpdateUI(hp / (float)maxHealth);
    }

    public void ResetHealth()
    {
        hp = maxHealth;
    }
}
