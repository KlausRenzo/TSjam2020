using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDie : MonoBehaviour
{
    //public string deathAnimation;
    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<Character>();
        if(character != null)
        {
            character.survival.Die();
        }
    }
}
