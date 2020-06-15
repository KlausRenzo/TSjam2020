using System;
using Assets.Scripts.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Managers
{
	public class SoundManager : Manager
	{
		[SerializeField] private Sound soundtrack;
		
		

		protected override void OnManagerDestroy()
		{
		}

		protected override void OnManagerAwake()
		{
			var source = gameObject.AddComponent<AudioSource>();
			source.loop = true;
			soundtrack.Play(source);
		}


	}
}