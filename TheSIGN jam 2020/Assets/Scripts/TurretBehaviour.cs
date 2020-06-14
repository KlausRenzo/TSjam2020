using System;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Transform mobilePart;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float fireRate = 1;
    [SerializeField] private Transform barrelTip;
    
    private float timer;
    private Vector3 startedForward;
    [SerializeField] private float treshOldDot = 0.5f;

    private void Awake()
    {
        startedForward = transform.forward;
    }

    private void Update()
    {
        if(!IsTargetInArea()) return;
        if(!IsPlayerInFrontOfTarget()) return;
        
        LookTowardsTarget();
        
        if (CanShoot())
        {
            Shoot();
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

    private bool IsTargetInArea()
    {
        Vector3 dir = (target.position - barrelTip.position).normalized;
        
        Ray ray = new Ray(barrelTip.position,dir);
        Debug.DrawRay(barrelTip.position,dir, Color.yellow, float.PositiveInfinity);
        
        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
        {
            Debug.Log($"turret hitted {hit.collider.name}");
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        
        return false;
    }
    
    private void Shoot()
    {
        Projectile projectileGo = Instantiate(projectile, barrelTip.position, Quaternion.identity);
        projectileGo.SetVelocity(mobilePart.forward);
    }

    
    private void LookTowardsTarget()
    {
        var rotationDirection = Vector3.RotateTowards(mobilePart.forward, target.position - barrelTip.position, rotationSpeed * Time.deltaTime, 0);
        mobilePart.rotation = Quaternion.LookRotation(rotationDirection);
    }

    private bool IsPlayerInFrontOfTarget()
    {
        float dot = Vector3.Dot(startedForward, (target.position - transform.position).normalized);
        Debug.Log($"{dot}");
        
        return dot > treshOldDot;
    }
}