using System;
using UnityEngine;
using UnityStandardAssets.Effects;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 50f;
    public float explosionRadius = 0f;
    public int damage = 50;

    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distancePerFrame)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        
        
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
                Damage(collider.transform);
        }
    }
    void Damage(Transform enemy)
    {
        Enemies _enemy = enemy.GetComponent<Enemies>();

        if (_enemy != null)
        {
            _enemy.TakeDamage(damage);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
