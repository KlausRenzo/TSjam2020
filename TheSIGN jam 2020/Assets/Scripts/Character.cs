using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStats stats;
    public CharacterEntity entity;
    public CharacterLocomotion locomotion;
    
    void Awake()
    {
        locomotion = GetComponent<CharacterLocomotion>();
        locomotion.character = this;
        entity = GetComponent<CharacterEntity>();
        entity.character = this;
    }

    void Update()
    {
        
    }

    public void Die()
    {

    }
}
