using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStats stats;

    public CharacterLocomotion locomotion;

    void Awake()
    {
        locomotion = GetComponent<CharacterLocomotion>();
        locomotion.character = this;

    }

    void Update()
    {
        
    }
}
