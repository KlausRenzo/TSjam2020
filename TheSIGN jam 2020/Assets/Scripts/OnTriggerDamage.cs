using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDamage : MonoBehaviour
{
	[SerializeField] private float howOftenDamage = .7f;
	[SerializeField] private int damageAmountPerTic = 1;

	private void OnTriggerEnter(Collider other)
	{
		var character = other.GetComponent<Character>();
		if (character != null)
		{
			if (character.stats.isFireProof)
			{
				StartCoroutine(character.survival.TakeDamageOverTime(damageAmountPerTic, howOftenDamage));
			}
			else
			{
				character.survival.TakeDamage(100); //shottato malissimo
			}
		}
	}
}