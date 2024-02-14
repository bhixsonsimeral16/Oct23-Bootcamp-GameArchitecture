using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;

    public Action<float> onHealthUpdated;
    public Action onDeath;

    public bool isDead { get; private set; } = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        onHealthUpdated?.Invoke(currentHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void DeductHealth(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            isDead = true;
            onDeath?.Invoke();
            currentHealth = 0f;
        }

        onHealthUpdated?.Invoke(currentHealth);
    }
}
