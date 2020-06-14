using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class TriggerNextLevel : MonoBehaviour
{
	[SerializeField] private string levelNameToLoad;

	private void Awake()
	{
		GetComponent<MeshRenderer>().enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Character>())
		{
			LoadScene(levelNameToLoad);
		}
	}

	[Button]
	private void LoadScene(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
}
