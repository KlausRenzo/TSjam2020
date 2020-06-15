using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField] private float projectileSpeed = 2;
	[SerializeField] private ParticleSystem hitParticle;
	[SerializeField] private Sound startSound;
	[SerializeField] private Sound hitSound;
	
	public event Action PlayerHitted;
	private AudioSource source;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		source = GetComponent<AudioSource>();
		startSound.Play(source);
	} 
	
	public void SetVelocity(Vector3 dir)
	{
		transform.LookAt(dir);
		rb.velocity = dir * projectileSpeed;
	}

	private void OnCollisionEnter(Collision other)
	{
		HandleDestruction();
	}

	private void HandleDestruction()
	{
		var particle = Instantiate(this.hitParticle, transform.position,Quaternion.identity);
		particle.Play();
		
		hitSound.Play(source);
		
		Destroy(gameObject, hitSound.ClipLenght);
	}
}