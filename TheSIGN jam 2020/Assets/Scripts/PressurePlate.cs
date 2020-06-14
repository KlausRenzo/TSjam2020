using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PressurePlate : SerializedMonoBehaviour
{
	[SerializeField] private IUnlockable item;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			item.Unlock();
		}
	}
}