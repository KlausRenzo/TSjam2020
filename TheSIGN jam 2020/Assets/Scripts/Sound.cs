using System.Collections;
using Sirenix.OdinInspector;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Sound : ScriptableObject
{
	[SerializeField] private AudioClip clip;
	[SerializeField, MinMaxSlider(0, 2)] private Vector2 volume = new Vector2(.8f, 1.2f);
	[SerializeField, MinMaxSlider(0, 2)] private Vector2 pitch = new Vector2(.8f, 1.2f);

	public void Play(AudioSource source)
	{
		SetUpValues(source);
		source.Play();
	}

	private void SetUpValues(AudioSource source)
	{
		source.clip = clip;
		source.volume = Random.Range(volume.x, volume.y);
		source.pitch = Random.Range(pitch.x, pitch.y);
	}
	
	
#if UNITY_EDITOR
	
	[Button("Preview Sound")]
	private void Preview()
	{
		EditorUtility.audioMasterMute = false;
		
		if(clip == null)
		{
			Debug.LogError($"Clip previewing is empty!");
			return;
		}

		var source = CreateObjectWithAudioSource();
		SetUpValues(source);
		source.Play();
		EditorCoroutineUtility.StartCoroutine(DestroyEditor(clip.length, source.gameObject), this);
	}

	private IEnumerator DestroyEditor(float time, GameObject go)
	{
		EditorApplication.playModeStateChanged += state => { ImmediateDestroyWhenPlaying(go, state); };
		yield return new EditorWaitForSeconds(time);
		DestroyImmediate(go);
	}

	private static void ImmediateDestroyWhenPlaying(GameObject go, PlayModeStateChange state)
	{
		if (state == PlayModeStateChange.ExitingEditMode)
			DestroyImmediate(go);
	}
	
	private AudioSource CreateObjectWithAudioSource()
	{
		GameObject holder = new GameObject($"{this.name} Sound");
		var source = holder.AddComponent<AudioSource>();
		return source;
	}
#endif
}