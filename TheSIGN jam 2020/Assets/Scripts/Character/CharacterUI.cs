using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    //public Image baseImage;
    public Color selected;
    public Color notSelected;
    [SerializeField] private List<Image> images = new List<Image>();

    private Vector3 startedScale;
    [SerializeField] private Vector3 highlightedScale;
    [SerializeField]private float animationDuration;
    [SerializeField] private Ease ease;

    private void Awake()
    {
        startedScale = images[0].transform.localScale;
    }

    public void UpdateImages(int selectedOne)
    {
        foreach(var img in images)
        {
            img.color = notSelected;
            img.rectTransform.DOScale(startedScale, animationDuration).SetEase(ease);
        }
        images[selectedOne].color = selected;
        images[selectedOne].rectTransform.DOScale(highlightedScale, animationDuration).SetEase(ease);
    }

    public void InitialiteSprites(List<Sprite> sprites)
    {
        for(int i = 0; i < sprites.Count; i++)
        {
            images[i].sprite = sprites[i];
        }
    }
}

