using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    
    [Header("Unity Setup Fields")]
    
    public string enemyTag = "Enemy";
    public Transform rotateRoot;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform fireRoot;
    // Start is called before the first frame update
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
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotateRoot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotateRoot.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        Debug.Log("SHOOT!");
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
