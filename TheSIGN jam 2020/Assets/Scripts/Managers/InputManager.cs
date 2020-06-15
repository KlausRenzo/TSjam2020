using System;
using Assets.Scripts.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Managers
{
	public class InputManager : Manager
	{
		private new Camera camera;
		[SerializeField] private LayerMask raycastLayer;

		public CharacterEntity ActiveEntity { get; set; }

		protected override void OnManagerDestroy()
		{
		}

		protected override void OnManagerAwake()
		{
			camera = Camera.main;
		}

		private void Update()
		{
			//	if (Input.GetButtonDown("MouseClick"))
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				Ray screenPointToRay = camera.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(screenPointToRay.origin, screenPointToRay.direction, out RaycastHit hit, Mathf.Infinity, raycastLayer))
				{
                    Debug.Log(hit.collider.name);
					Vector3 point = hit.point;
					InputAction action = new InputAction()
					{
						position = point,
						time = Time.time
					};
					ActiveEntity.RegisterAction(action);
                    ActiveEntity.ExectuteAction(action);
				}
			}
		}


	}
}