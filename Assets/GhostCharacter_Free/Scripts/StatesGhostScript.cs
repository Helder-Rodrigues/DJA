using UnityEngine;
using UnityEngine.AI;

public class GhostScript : MonoBehaviour
{
    public Transform player; 
    public Transform[] otherGhosts; 
    public float detectionRange = 10f; // Raio para detectar o jogador
    public float ghostAvoidRange = 5f; // Raio para evitar outros fantasmas

    private NavMeshAgent agent;

    private enum State
    {
        Wander, 
        ChasePlayer,
        AvoidGhost 
    }
    private State currentState; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Wander; // Estado inicial
    }

    void Update()
    {
        // Atualizar o estado baseado nas condições
        UpdateState();

        
        switch (currentState)
        {
            case State.Wander:
                Wander();
                break;
            case State.ChasePlayer:
                ChasePlayer();
                break;
            case State.AvoidGhost:
                AvoidGhost();
                break;
        }
    }

    private void UpdateState()
    {
        if (Vector3.Distance(player.position, transform.position) <= detectionRange)
        {
            currentState = State.ChasePlayer; 
        }
        else
        {
            foreach (var ghost in otherGhosts)
            {
                if (Vector3.Distance(ghost.position, transform.position) <= ghostAvoidRange)
                {
                    currentState = State.AvoidGhost; // Detecta outro fantasma, muda
                    return; 
                }
            }
            currentState = State.Wander; // Caso contrário, continua em "Wander"
        }
    }

    
    private void Wander()
    {
        if (!agent.hasPath)
        {
            // Define um destino aleatório no NavMesh
            Vector3 randomDirection = Random.insideUnitSphere * 10f; // Raio de 10
            randomDirection += transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 10f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AvoidGhost()
    {
        Vector3 avoidDirection = Vector3.zero;

        foreach (var ghost in otherGhosts)
        {
            float distance = Vector3.Distance(ghost.position, transform.position);
            if (distance <= ghostAvoidRange)
            {
                avoidDirection += transform.position - ghost.position; // Direção contrária ao fantasma
            }
        }

        avoidDirection.Normalize(); // Normaliza para evitar problemas com soma vetorial
        Vector3 newDestination = transform.position + avoidDirection * 5f; // Move para longe
        if (NavMesh.SamplePosition(newDestination, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}

