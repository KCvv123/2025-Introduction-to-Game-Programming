using UnityEngine;
using UnityEngine.AI;

public class MonAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    private UnityEngine.AI.NavMeshAgent agent;
    private GameManager gameManager;
    private int currentP = 0;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gameManager = FindFirstObjectByType<GameManager>();

        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentP].position;

        currentP = (currentP + 1) % patrolPoints.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("扣血");
            
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1);                
            }
        }
    }
}
