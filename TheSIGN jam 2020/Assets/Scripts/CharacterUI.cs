using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    //public Image baseImage;
    public Color selected;
    public Color notSelected;
    [SerializeField] private List<Image> images = new List<Image>();
    public void UpdateImages(int selectedOne)
    {
        foreach(var img in images)
        {
            img.color = notSelected;
        }
        images[selectedOne].color = selected;
    }

    public void InitialiteSprites(List<Sprite> sprites)
    {
        for(int i = 0; i < sprites.Count; i++)
        {
            images[i].sprite = sprites[i];
        }
    }
}
