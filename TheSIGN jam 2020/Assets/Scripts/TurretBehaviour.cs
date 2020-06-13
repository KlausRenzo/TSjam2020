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

    private void Update()
    {
        if(!IsTargetInArea()) return;
        
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
        Vector3 dir = (target.position - transform.position).normalized;
        
        Debug.DrawLine(transform.position, dir);
        
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, float.PositiveInfinity))
        {
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
        var rotationDirection = Vector3.RotateTowards(mobilePart.forward, target.position, rotationSpeed * Time.deltaTime, 0);
        mobilePart.rotation = Quaternion.LookRotation(rotationDirection);
    }
}