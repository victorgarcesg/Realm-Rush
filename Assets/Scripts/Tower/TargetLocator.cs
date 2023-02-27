using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 15f;
    Transform target;

    private void Start()
    {
        Attack(false);
    }
    
    private void Update()
    {
        FindClosestEnemy();
        AimWeapon();
    }

    private void FindClosestEnemy()
    {
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            var enemy = enemies[i];
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    private void AimWeapon()
    {
        if (target == null)
        {
            return;
        }

        float targetDistance = Vector3.Distance(transform.position, target.position);
        bool canAttack = targetDistance <= range;
        Attack(canAttack);
        weapon.LookAt(target);
    }

    private void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
 