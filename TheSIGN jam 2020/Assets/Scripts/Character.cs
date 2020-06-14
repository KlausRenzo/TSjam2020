using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStats stats;
    public CharacterEntity entity;
    public CharacterLocomotion locomotion;
    public AnimationHandler animationHandler;
    
    void Awake()
    {
        locomotion = GetComponent<CharacterLocomotion>();
        locomotion.character = this;
        entity = GetComponent<CharacterEntity>();
        entity.character = this;
        animationHandler = GetComponent<AnimationHandler>();
    }

    void Update()
    {
        
    }

    public void Die()
    {

    }
}
