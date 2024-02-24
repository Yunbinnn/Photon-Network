using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Metalon : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Turret Tower").transform;
    }

    void Update()
    {
        Move();   
    }

    public void Move()
    {
        agent.SetDestination(target.position);
    }
}
