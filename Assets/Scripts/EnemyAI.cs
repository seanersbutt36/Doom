using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyAwareness enemyAwareness;
    private Transform playerTransform;
    private NavMeshAgent agent;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playerTransform = FindAnyObjectByType<PlayerMove>().transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (enemyAwareness.isAggro)
        {
            agent.SetDestination(playerTransform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
}
