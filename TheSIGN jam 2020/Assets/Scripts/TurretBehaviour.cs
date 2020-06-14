﻿using System;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField] private Transform [] targets;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Transform mobilePart;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float fireRate = 1;
    [SerializeField] private Transform barrelTip;
    private CharacterState currentState = CharacterState.alive;
    private float timer;
    private Vector3 startedForward;
    [SerializeField] private float treshOldDot = 0.5f;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        startedForward = transform.forward;
    }

    private void Update()
    {
        if (currentState == CharacterState.alive)
        {
            Transform actualTarget = IsTargetInArea();
            if (actualTarget == null) return;

            if (!IsPlayerInFrontOfTurret(actualTarget)) return;

            LookTowardsTarget(actualTarget);

            if (CanShoot())
            {
                Shoot();
            }
        }
    }

    
    private bool CanShoot()
    {
        if (timer < fireRate)
        {
            timer += Time.deltaTime;
            return false;
        }
        else
        {
            timer = 0;
            return true;
        }    
    }

    private Transform IsTargetInArea()
    {
        foreach (Transform target in targets)
        {
            Vector3 dir = (target.position - barrelTip.position).normalized;
        
            Ray ray = new Ray(barrelTip.position,dir);
            Debug.DrawRay(barrelTip.position,dir, Color.yellow, float.PositiveInfinity);
        
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
            {
                Debug.Log($"turret hitted {hit.collider.name}");
                if (hit.collider.CompareTag("Player"))
                {
                    return target;
                }
            }
        }
        return null;
    }
    
    private void Shoot()
    {
        Projectile projectileGo = Instantiate(projectile, barrelTip.position, Quaternion.identity);
        projectileGo.SetVelocity(mobilePart.forward);
    }

    
    private void LookTowardsTarget(Transform target)
    {
        var rotationDirection = Vector3.RotateTowards(mobilePart.forward, target.position - barrelTip.position, rotationSpeed * Time.deltaTime, 0);
        mobilePart.rotation = Quaternion.LookRotation(rotationDirection);
    }

    private bool IsPlayerInFrontOfTurret(Transform target)
    {
        float dot = Vector3.Dot(startedForward, (target.position - transform.position).normalized);
        Debug.Log($"{dot}");
        
        return dot > treshOldDot;
    }

    public void EnableTurret(bool b)
    {
        currentState = (b) ? CharacterState.alive : CharacterState.dead;
        anim.Play((b) ? "idle" : "destroied");
    }
}

