﻿using System;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStats stats;
    public CharacterEntity entity;
    public CharacterLocomotion locomotion;
    public CharacterSurvival survival;
    public AnimationHandler animationHandler;
    public Sprite icon;

    public CharacterState currentState = CharacterState.alive;

    public event Action PlayerResetted;
    
    void Awake()
    {
        locomotion = GetComponent<CharacterLocomotion>();
        locomotion.character = this;
        entity = GetComponent<CharacterEntity>();
        entity.character = this;
        animationHandler = GetComponent<AnimationHandler>();
        survival = GetComponent<CharacterSurvival>();
        survival.character = this;
    }

    public void Reset()
    {
        currentState = CharacterState.alive;
        animationHandler.Play(animationHandler.idleAnimation);
        survival.ResetHealth();
        PlayerResetted?.Invoke();
    }
}

public enum CharacterState { alive, dead };
