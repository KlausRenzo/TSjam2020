using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField] private float projectileSpeed = 2;

	public event Action PlayerHitted;
	
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	} 
	
	public void SetVelocity(Vector3 dir)
	{
		transform.LookAt(dir);
		rb.velocity = dir * projectileSpeed;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerHitted?.Invoke();
		}
	}
}