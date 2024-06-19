using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour
{
    public Transform enemyDestination;
    private NavMeshAgent myAgent;
    
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        myAgent.destination = enemyDestination.position;
    }
}
