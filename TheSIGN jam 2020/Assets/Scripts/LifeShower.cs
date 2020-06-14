using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class LifeShower : MonoBehaviour
{

	#region Fields

	// Static

	// Public

	// Hidden Public
	[SerializeField]private CharacterSurvival survival;
	[SerializeField] private float heightAbovePlayer;
	[SerializeField] private Image healthImagePrefab;
	
	private RectTransform rectTransform;
	private new Camera camera;
	private Image[] healthImages;
	// Properties

	// Components

	// Events

	#endregion

	#region Unity Callbacks

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		camera = Camera.main;
		healthImages = new Image[survival.CurrentHealth];
		InstantiateImages();
	}

	private void OnEnable()
	{
		survival.PlayerDamagedBeforeHealthIsSet += OnPlayerDamaged;
	}

	private void OnDisable()
	{
		survival.PlayerDamagedBeforeHealthIsSet -= OnPlayerDamaged;
	}

	private void Update()
	{
		Vector3 playerToUIPos = camera.WorldToScreenPoint(survival.transform.position);
		rectTransform.position = playerToUIPos + new Vector3(0,heightAbovePlayer,0);
	}

	#endregion

	#region Methods

	private void InstantiateImages()
	{
		for (int i = 0; i < healthImages.Length; i++)
		{
			healthImages[i] = Instantiate(healthImagePrefab, transform);
		}
	}

	[Button]
	private void OnPlayerDamaged(int position)
	{
		if(position == 0) return;	// se è morto e dovesse essere reinvocato l'evento non deve fa nulla 
		
		position -= 1;	// array starts at 0, noobie
		
		healthImages[position].color = Color.black;
		
	}
	#endregion

}