using System;
using Assets.Scripts.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Managers
{
	public class InputManager : Manager
	{
		[SerializeField] private Camera camera;
		[SerializeField] private LayerMask raycastLayer;

		public CharacterEntity ActiveEntity { get; set; }

		protected override void OnManagerDestroy()
		{
		}

		protected override void OnManagerAwake()
		{
		}

		private void Update()
		{
			//	if (Input.GetButtonDown("MouseClick"))
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				Ray screenPointToRay = camera.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(screenPointToRay.origin, screenPointToRay.direction, out RaycastHit hit, Mathf.Infinity, raycastLayer))
				{
					Vector3 point = hit.point;
					InputAction action = new InputAction()
					{
						position = point,
						time = Time.time
					};
					// TODO: Call registered user action
					ActiveEntity.RegisterAction(action);
                    ActiveEntity.ExectuteAction(action);
				}
			}
		}


	}
}