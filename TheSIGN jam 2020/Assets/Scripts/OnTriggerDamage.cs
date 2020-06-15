using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<Character>();
        if(character != null)
        {
            if (character.stats.isFireProof)
            {
               DamageOverTime();
            }
            else
            {
                character.survival.Die();
            }
        }
    }

    private void DamageOverTime()
    {
        return;
    }
}
