using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    public TMP_Text healthText;
    public GameObject gameOverText;

    void OnEnable()
    {
        playerHealth.onHealthUpdated += OnHealthUpdate;
        playerHealth.onDeath += OnDeath;
    }

    void OnDestroy()
    {
        playerHealth.onHealthUpdated -= OnHealthUpdate;
        playerHealth.onDeath -= OnDeath;
    }

    void Start()
    {
         gameOverText.SetActive(false);
    }

    void OnHealthUpdate(float health)
    {
        healthText.text = "Health : " + Mathf.Floor(health).ToString();
    }

    void OnDeath()
    {
        gameOverText.SetActive(true);
    }
}
