using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterSurvival : MonoBehaviour
{
    public Character character;
    [ShowInInspector]private int currentHealth;

    public event Action<int> PlayerDamagedBeforeHealthIsSet;
    public int CurrentHealth => currentHealth;
    
    void Awake()
    {
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
        PlayerDamagedBeforeHealthIsSet?.Invoke(currentHealth);
        
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, character.stats.health);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die(string animationName = null)
    {
        character.currentState = CharacterState.dead;

        string animationToPlay = (animationName == null) ? character.animationHandler.deathAnimation : animationName;
        character.animationHandler.Play(animationToPlay);
    }
    public void ResetHealth()
    {
        currentHealth = character.stats.health;
    }
}