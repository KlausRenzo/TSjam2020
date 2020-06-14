using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSurvival : MonoBehaviour
{
    public Character character;
    public Action OnDeath;
    private int currentHealth;

    void Start()
    {
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
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
        Invoke("CallOnDeath", 0.5f);
    }
    public void ResetHealth()
    {
        currentHealth = character.stats.health;
    }

    public void CallOnDeath() => OnDeath?.Invoke();
}
