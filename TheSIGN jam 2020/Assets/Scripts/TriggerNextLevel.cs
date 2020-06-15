using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
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
	private bool hasBeenLockedSinceBeginning;

	private void Awake()
	{
		GetComponent<MeshRenderer>().enabled = false;
		startRotation = rotationAnchor.rotation;
		hasBeenLockedSinceBeginning = isUnlocked;
	}

	private void Start()
	{
		if (isUnlocked)
		{
			rotationAnchor.DORotate(new Vector3(animationRotationAngle, 0, 0), animationDuration + 1.5f).SetEase(animationEase);
		}
	}

	private void OnEnable()
	{
		ServiceLocator.Locate<GameManager>().OnReset += Reset;
	}

	private void OnDisable()
	{
		ServiceLocator.Locate<GameManager>().OnReset -= Reset;
	}

	private void OnTriggerEnter(Collider other)
	{
		var character = other.GetComponent<Character>(); 
		if (character && isUnlocked)
		{
			if(character.stats.name == "PlayerTarget")
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
		
		//non in tutte le scene è lockata la sbarra
		if (!hasBeenLockedSinceBeginning)
		{
			isUnlocked = false;
		}
	}
}