using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public float initialSpeed = 10f;
    [HideInInspector]
    public float speed;
    
    public float startHealth = 100;
    private float health;
    [FormerlySerializedAs("moneyGain")] 
    public int worth = 50;
    
    public GameObject deathEffect;

    [Header("Unity Settings")] 
    public Image healthBar;

    void Start()
    {
        speed = initialSpeed;
        health = startHealth;
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0) Die();
    }
    
    public void Slow(float slowFactor)
    {
        speed = initialSpeed * (1f - slowFactor);
    }

    void Die()
    {
        PlayerStats.Money += worth;
        
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        WaveSpawner.EnemiesAlive--;
        
        Destroy(gameObject);
    }
    
}
