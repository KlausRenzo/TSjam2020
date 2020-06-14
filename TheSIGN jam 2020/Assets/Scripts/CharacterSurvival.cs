using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSurvival : MonoBehaviour
{
    public Character character;
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
    }
    public void ResetHealth()
    {
        currentHealth = character.stats.health;
    }
}
