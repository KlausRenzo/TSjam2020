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
		private List<InputAction> history = new List<InputAction>();

		private Vector3 startingPosition;
		//private Character character;

		private void Start()
		{
			//character = this.GetComponent<Character>();
			SetActiveCharacter();
			startingPosition = transform.position;
		}

		public void DoAction(InputAction inputAction)
		{
			history.Add(inputAction);
			ExectuteAction(inputAction);
			Debug.Log(inputAction.position + " " + inputAction.time );
		}

		public void SetActiveCharacter()
		{
			ServiceLocator.Locate<InputManager>().ActiveEntity = this;
			history.Add(new InputAction()
			{
				position = transform.position,
				time = Time.time
			});
		}

		private IEnumerator PlaybackActionHistory()
		{
			InputAction lastAction = history[0];
			foreach (var currentAction in history.Skip(1))
			{
				float actionTimeDelta = currentAction.time - lastAction.time;
				yield return new WaitForSeconds(actionTimeDelta);
				ExectuteAction(currentAction);
				lastAction = currentAction;
			}
		}

		private void ExectuteAction(InputAction action)
		{
			transform.position = action.position;
			//todo: ChangeAction
			//character.destination = action.position;
		}

		[Button]
		public void GOJHONNYGO()
		{
			transform.position = startingPosition;
			StartCoroutine(PlaybackActionHistory());
		}
	}

}