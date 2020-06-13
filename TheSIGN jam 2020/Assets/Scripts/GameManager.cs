using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using System;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public Action OnCharacterDeath;
    public List<Character> characters = new List<Character>();
    public Transform spawnPoint;
    private int currentCharacter = -1;

    public void Start()
    {
        SetNextCharacter();
    }

    [Button]
    public void SetNextCharacter()
    {
        if(currentCharacter != -1)
        {
            OnCharacterDeath -= characters[currentCharacter].Die;
        }
        currentCharacter++;
        if(currentCharacter > characters.Count)
        {
            //Die!
        }
        for(int i = 0; i <= currentCharacter; i++)
        {
            characters[i].gameObject.SetActive(true);
            characters[i].transform.position = spawnPoint.position;
            //todo resettare a modino
        }
        ServiceLocator.Locate<InputManager>().ActiveEntity = characters[currentCharacter].entity;
        for (int i = 0; i < currentCharacter; i++)
        {
            characters[i].entity.GOJHONNYGO();
        }
        OnCharacterDeath += characters[currentCharacter].Die;
    }
}
