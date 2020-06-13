using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
	public abstract class Manager : SerializedMonoBehaviour
	{
		public bool isPersistent;
		private void Awake()
		{
			if (!ServiceLocator.Register(this))
			{
				Destroy(this.gameObject);
				return;
			}

			OnManagerAwake();
			SceneManager.sceneUnloaded += OnSceneUnloaded;
		}

		private void OnSceneUnloaded(Scene scene)
		{
			SceneManager.sceneUnloaded -= OnSceneUnloaded;
			
			if (isPersistent)
				return;
			ServiceLocator.UnRegister(this);
		}
		
		private void OnDestroy()
		{
			OnManagerDestroy();
		}

		protected abstract void OnManagerDestroy();

		protected abstract void OnManagerAwake();
	}
}