using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    NavMeshAgent enemy;
    public Transform player;

    private void Awake()
    {
        enemy = this.GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        enemy.SetDestination(player.position);
    }
}
