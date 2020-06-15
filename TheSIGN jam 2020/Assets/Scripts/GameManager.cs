using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using System;
using Sirenix.OdinInspector;

public class GameManager : Manager
{
    public event Action OnReset;
    public List<Character> characters = new List<Character>();
    public Transform spawnPoint;
    public CharacterUI characterUi;
    private int currentCharacterNumber = -1;
    private List<Collider> charactersColliders = new List<Collider>();

    protected override void OnManagerDestroy()
    {
    }

    protected override void OnManagerAwake()
    {
    }

    public void Start()
    {
        SetCollisionAvoidance();
        SetNextCharacter();
        var iconList = new List<Sprite>();
        foreach(var c in characters)
        {
            iconList.Add(c.icon);
        }
        characterUi.InitialiteSprites(iconList);
    }

    [Button]
    public void SetNextCharacter()
    {
        if(currentCharacterNumber != -1)
        {
            characters[currentCharacterNumber].survival.OnDeath -= SetNextCharacter;
        }
        currentCharacterNumber++;
        if(currentCharacterNumber > characters.Count-1)
        {
            return;
        }
        for(int i = 0; i <= currentCharacterNumber; i++)
        {
            characters[i].gameObject.SetActive(true);
            characters[i].transform.position = spawnPoint.position;
            characters[i].Reset();
            OnReset?.Invoke();
            //todo resettare a modino
        }
        ServiceLocator.Locate<InputManager>().ActiveEntity = characters[currentCharacterNumber].entity;
        for (int i = 0; i < currentCharacterNumber; i++)
        {
            characters[i].entity.GOJHONNYGO();
        }
        characters[currentCharacterNumber].survival.OnDeath += SetNextCharacter;

        characterUi.UpdateImages(currentCharacterNumber);
    }
    public void SetCollisionAvoidance()
    {
        foreach(var c in characters)
        {
            c.gameObject.SetActive(true);
            charactersColliders.Add(c.GetComponent<Collider>());
        }
        for(int i = 0; i < charactersColliders.Count-1; i++)
        {
            for (int j = i + 1; j < charactersColliders.Count; j++)
            {
                Physics.IgnoreCollision(charactersColliders[i], charactersColliders[j],true);
            }
        }
        foreach(var c in characters)
        {
            c.gameObject.SetActive(false);
        }
    }
}
