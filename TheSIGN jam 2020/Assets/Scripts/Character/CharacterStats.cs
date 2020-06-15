using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats")]
public class CharacterStats : ScriptableObject
{
    public float speed;
    public int health;
    public bool isFireProof;
}
