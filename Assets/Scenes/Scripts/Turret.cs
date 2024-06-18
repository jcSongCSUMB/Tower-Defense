using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemies targetEnemy;

    [Header("General")]
    public float range = 15f;
    
    [Header("use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("use Laser")] 
    public bool useLaser = default;

    public int damageOverTime = 30;
    public float slowFactor = .2f;
    
    public LineRenderer linerenderer;
    public ParticleSystem impactEffect;
    
    [Header("Unity Setup Fields")]
    
    public string enemyTag = "Enemy";
    public Transform rotateRoot;
    public float turnSpeed = 10f;
    
    public Transform fireRoot;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if (nearestEnemy != null && minDistance <= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemies>();
            }
            else
            {
                target = null;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (linerenderer.enabled)
                {
                    impactEffect.Stop();
                    linerenderer.enabled = false;
                }
                    
            }
            
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotateRoot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotateRoot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowFactor);
        
        if (!linerenderer.enabled)
        {
            linerenderer.enabled = true;
            impactEffect.Play();
        }
        
        linerenderer.SetPosition(0, fireRoot.position);
        linerenderer.SetPosition(1, target.position);

        Vector3 dir = fireRoot.position - target.position;
        
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        impactEffect.transform.position = target.position + dir.normalized;
    }

    void Shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, fireRoot.position, fireRoot.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
