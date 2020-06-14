using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class TriggerNextLevel : MonoBehaviour, IUnlockable
{
	[SerializeField] private string levelNameToLoad;
	[SerializeField] private bool isUnlocked = true;

	[SerializeField] private Transform rotationAnchor;
	[SerializeField] private float animationRotationAngle = 50;
	[SerializeField] private float animationDuration = 2;
	[SerializeField] private Ease animationEase;

	private Quaternion startRotation;

	private void Awake()
	{
		GetComponent<MeshRenderer>().enabled = false;
		startRotation = rotationAnchor.rotation;
	}

	private void Start()
	{
		if (isUnlocked)
		{
			rotationAnchor.DORotate(new Vector3(animationRotationAngle, 0, 0), animationDuration + 1).SetEase(animationEase);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Character>() && isUnlocked)
		{
			LoadScene(levelNameToLoad);
		}
	}

	private void LoadScene(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	[Button]
	public void Unlock()
	{
		rotationAnchor.DORotate(new Vector3(animationRotationAngle, 0, 0), animationDuration).SetEase(animationEase);
		isUnlocked = true;
	}

	[Button]
	public void Reset()
	{
		rotationAnchor.rotation = startRotation;
	}
}