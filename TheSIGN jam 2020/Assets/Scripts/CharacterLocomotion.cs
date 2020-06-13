using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class CharacterLocomotion : MonoBehaviour
{
    public Character character;
    public List<Vector3> path = new List<Vector3>();
    private NavMeshAgent agent;
    [SerializeField]private float maxDistance = 0.05f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.speed = character.stats.speed;
    }

    public void Update()
    {

        if (path.Count > 0)
        {
            var dir = (path[0] - transform.position).normalized;
            var distanceFromCurrentPoint = Vector3.Distance(transform.position, path[0]);
            var newPos = transform.position + dir * Time.deltaTime * character.stats.speed;
            var distanceFromNewPos = Vector3.Distance(transform.position, newPos);
            transform.position += dir * (((distanceFromNewPos > distanceFromCurrentPoint)) ? distanceFromCurrentPoint : distanceFromNewPos);
            transform.LookAt(transform.position + dir);
            if (Vector3.Distance(transform.position, path[0]) < maxDistance)
            {
                path.RemoveAt(0);
            }
        }
    }

    public void ChangeTarget(Vector3 target)
    {
        agent.SetDestination(target);
        path = agent.path.corners.ToList();
        foreach(var x in path)
        {
            Debug.Log(x);
        }
    }
}
