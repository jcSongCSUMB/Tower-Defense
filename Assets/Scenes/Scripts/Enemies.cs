using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemies : MonoBehaviour
{
    public float initialSpeed = 10f;
    [HideInInspector]
    public float speed;
    
    public float health = 100;
    [FormerlySerializedAs("moneyGain")] 
    public int worth = 50;
    
    public GameObject deathEffect;


    void Start()
    {
        speed = initialSpeed;
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount;

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
        
        Destroy(gameObject);
    }
    
}
