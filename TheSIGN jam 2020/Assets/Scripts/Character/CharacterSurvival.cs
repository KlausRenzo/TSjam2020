using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterSurvival : MonoBehaviour
{
    public Character character;
    [ShowInInspector]private int currentHealth;
    public Action OnDeath;

    public event Action<int,int> PlayerDamagedBeforeHealthIsSet;
    public int CurrentHealth => currentHealth;
    
    void Awake()
    {
        ResetHealth();
    }

    public void TakeDamage(int damageAmount)
    {
        PlayerDamagedBeforeHealthIsSet?.Invoke(currentHealth,damageAmount);
        
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, character.stats.health);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public IEnumerator TakeDamageOverTime(int damageAmount,float howOftenDamage)
    {
        float timer = 0;
        
        while (character.currentState != CharacterState.dead)
        {
            
            if (timer >= howOftenDamage)
            {
                TakeDamage(damageAmount);
                timer = 0;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        StopCoroutine($"TakeDamageOverTime");
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
