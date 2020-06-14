using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class CharacterLocomotion : MonoBehaviour
{
    public Character character;
    public List<Vector3> pathCoordinates = new List<Vector3>();
    private NavMeshAgent agent;
    [SerializeField]private float maxDistance = 0.05f;
    private NavMeshPath navPath;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        navPath = new NavMeshPath();
        //agent.speed = character.stats.speed;
    }

    public void Update()
    {
        if (character.currentState == CharacterState.alive)
        {
            if (pathCoordinates.Count > 0)
            {
                var dir = (pathCoordinates[0] - transform.position).normalized;
                var distanceFromCurrentPoint = Vector3.Distance(transform.position, pathCoordinates[0]);
                var newPos = transform.position + dir * Time.deltaTime * character.stats.speed;
                var distanceFromNewPos = Vector3.Distance(transform.position, newPos);
                transform.position += dir * (((distanceFromNewPos > distanceFromCurrentPoint)) ? distanceFromCurrentPoint : distanceFromNewPos);
                transform.LookAt(transform.position + dir);
                character.animationHandler.Play(character.animationHandler.movementAnimation);
                if (Vector3.Distance(transform.position, pathCoordinates[0]) < maxDistance)
                {
                    pathCoordinates.RemoveAt(0);
                    if (pathCoordinates.Count == 0)
                    {
                        character.animationHandler.Play(character.animationHandler.idleAnimation);
                    }
                }
            }
        }
    }

    public void ChangeTarget(Vector3 target)
    {
        agent.ResetPath();
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, navPath);
        agent.SetPath(navPath);
        pathCoordinates = agent.path.corners.ToList();
    }
}
