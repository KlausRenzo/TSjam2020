using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Player
{
	public class CharacterEntity : MonoBehaviour
	{
        public Character character;
		private List<InputAction> history = new List<InputAction>();
        private Coroutine cor;

		private void Start()
		{
		}

		public void RegisterAction(InputAction inputAction)
		{
			history.Add(inputAction);
		}

		private IEnumerator PlaybackActionHistory()
		{
			InputAction lastAction = history[0];
			foreach (var currentAction in history)
			{
				float actionTimeDelta = currentAction.time - lastAction.time;
				yield return new WaitForSeconds(actionTimeDelta);
				ExectuteAction(currentAction);
				lastAction = currentAction;
			}
		}

		public void ExectuteAction(InputAction action)
		{
            character.locomotion.ChangeTarget(action.position);
		}

		[Button]
		public void GOJHONNYGO()
		{
            if(cor != null)
            {
                StopCoroutine(cor);
            }
			cor = StartCoroutine(PlaybackActionHistory());
		}
	}

}