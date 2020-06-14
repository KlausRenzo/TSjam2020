using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using System;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public Action<string> OnCharacterDeath;
    public List<Character> characters = new List<Character>();
    public Transform spawnPoint;
    private int currentCharacter = -1;
    private List<Collider> charactersColliders = new List<Collider>();

    public void Start()
    {
        SetCollisionAvoidance();
        SetNextCharacter();
    }

    [Button]
    public void SetNextCharacter()
    {
        if(currentCharacter != -1)
        {
            OnCharacterDeath -= characters[currentCharacter].survival.Die;
        }
        currentCharacter++;
        if(currentCharacter > characters.Count-1)
        {
            return;
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
        OnCharacterDeath += characters[currentCharacter].survival.Die;
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
