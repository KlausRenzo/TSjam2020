using Assets.Scripts.Managers;
using System;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
	[SerializeField] private Transform[] targets;
	[SerializeField] private float rotationSpeed = 10;
	[SerializeField] private Transform mobilePart;
	[SerializeField] private Projectile projectile;
	[SerializeField] private float fireRate = 1;
	[SerializeField] private Transform barrelTip;
	[SerializeField] private float range = 100;
	[SerializeField] private int damageAmountPerShot = 1;
	[SerializeField] private Sound deactivatedSound;

	public GameObject smokeParticle;
	private GameObject instantiatedSmoke;

	private CharacterState currentState = CharacterState.alive;
	private float timer;
	private Vector3 startedForward;
	[SerializeField] private float treshOldDot = 0.5f;
	private Animator anim;

	public event Action OnTurretShooted;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		startedForward = transform.forward;
		instantiatedSmoke = Instantiate(smokeParticle, transform.position, smokeParticle.transform.rotation);
		instantiatedSmoke.SetActive(false);
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
				Shoot(actualTarget.GetComponent<CharacterSurvival>());
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
			if (target.GetComponent<Character>().currentState == CharacterState.dead) continue;

			Vector3 dir = (target.position - barrelTip.position).normalized;

			Ray ray = new Ray(barrelTip.position, dir);
			Debug.DrawRay(barrelTip.position, dir * range, Color.yellow, 10);

			if (Physics.Raycast(ray, out RaycastHit hit, range))
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

	private void Shoot(CharacterSurvival survival)
	{
		Projectile projectileGo = Instantiate(projectile, barrelTip.position, Quaternion.identity);
		projectileGo.SetVelocity(mobilePart.forward);
		survival.TakeDamage(damageAmountPerShot);
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
		instantiatedSmoke?.SetActive(!b);
		if (!b)
		{
			deactivatedSound.Play(GetComponent<AudioSource>());

			ServiceLocator.Locate<GameManager>().OnReset += Reset;
		}
		else
		{
			ServiceLocator.Locate<GameManager>().OnReset -= Reset;
		}
	}

	public void Reset()
	{
		EnableTurret(true);
	}
}