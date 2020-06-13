using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterLocomotion : MonoBehaviour
{
    public Character character;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = character.stats.speed;
    }

    public void ChangeTarget(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
