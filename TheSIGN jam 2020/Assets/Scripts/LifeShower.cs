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
	[SerializeField] private Character character;
	[SerializeField] private float heightAbovePlayer;
	[SerializeField] private Image healthImagePrefab;
	private CharacterSurvival survival;
	private RectTransform rectTransform;
	private new Camera camera;
	private Image[] healthImages;
	private Color startedColor;
	// Properties

	// Components

	// Events

	#endregion

	#region Unity Callbacks

	private void Awake()
	{
		survival = character.survival;
		rectTransform = GetComponent<RectTransform>();
		camera = Camera.main;
		healthImages = new Image[survival.CurrentHealth];
		InstantiateImages();
		startedColor = healthImages[0].color;
	}

	private void OnEnable()
	{
		survival.PlayerDamagedBeforeHealthIsSet += OnPlayerDamaged;
		character.PlayerResetted += ResetUI;
	}

	private void OnDisable()
	{
		survival.PlayerDamagedBeforeHealthIsSet -= OnPlayerDamaged;
		character.PlayerResetted -= ResetUI;
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
	private void OnPlayerDamaged(int position,int damageAmount)
	{
		if(position == 0) return;	// se è morto e dovesse essere reinvocato l'evento non deve fa nulla
		
		//se mi hanno shottato levo direttamente tutto ez
		if(damageAmount == 100)
		{
			AllBlacks();
			return;
		}
		
		position -= 1;	// array starts at 0, noobie
		
		healthImages[position].color = Color.black;
	}

	private void ResetUI()
	{
		for (int i = 0; i < healthImages.Length; i++)
		{
			healthImages[i].color = startedColor;
		}
	}

	private void AllBlacks()
	{
		for (int i = 0; i < healthImages.Length; i++)
		{
			healthImages[i].color = Color.black;
		}
	}
	#endregion

}