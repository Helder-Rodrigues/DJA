using UnityEngine;
using UnityEngine.AI;

public class GhostScript : MonoBehaviour
{
    public Transform player;
    public Transform[] otherGhosts;
    public float detectionRange = 20f; // Raio para detectar o jogador
    public float ghostAvoidRange = 10f; // Raio para evitar outros fantasmas

    private NavMeshAgent agent;

    private enum State
    {
        Wander,
        ChasePlayer,
        AvoidGhost,
        Stuck
    }
    private State currentState;

    private Vector3 lastPosition;
    private float stuckTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Wander;
        lastPosition = transform.position;
        stuckTimer = 0f;
    }

    void Update()
    {
        if (currentState == State.Stuck)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Reset the stuck state after moving
                    currentState = State.Wander;
                    stuckTimer = 0f;
                }
        }
        else if (currentState == State.AvoidGhost)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    currentState = State.Wander;
        }
        else
        {
            // Update stuck timer and check for Stuck state
            UpdateStuckState();

            // Atualizar o estado baseado nas condições
            UpdateState();
            switch (currentState)
            {
                case State.Wander:
                    agent.isStopped = true;
                    Wander();
                    break;
                case State.ChasePlayer:
                    agent.isStopped = false;
                    gameObject.GetComponent<Ghost_Controller>().path.Clear();
                    ChasePlayer();
                    break;
                case State.AvoidGhost:
                    agent.isStopped = false;
                    gameObject.GetComponent<Ghost_Controller>().path.Clear();
                    AvoidGhost();
                    break;
                case State.Stuck:
                    agent.isStopped = false;
                    Debug.Log("Ghost is stuck, moving to a random nearby position.");
                    MoveToRandomNearbyPosition();
                    break;
            }
        }
    }

    private void UpdateStuckState()
    {
        // Check if the ghost is in the same position for 1 second
        if (Vector3.Distance(transform.position, lastPosition) < 0.1f)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= 1f)
            {
                currentState = State.Stuck;
            }
        }
        else
        {
            // Reset timer if ghost is moving
            stuckTimer = 0f;
            lastPosition = transform.position;
        }
    }

    private void UpdateState()
    {
        // Avoid processing other states if the ghost is stuck
        if (currentState == State.Stuck) return;

        foreach (var ghost in otherGhosts)
        {
            if (Vector3.Distance(ghost.position, transform.position) <= ghostAvoidRange)
            {
                currentState = State.AvoidGhost; // Detecta outro fantasma, muda
                return;
            }
        }

        //persegue o jogador se estiver perto
        if (Vector3.Distance(player.position, transform.position) <= detectionRange)
        {
            currentState = State.ChasePlayer;
            return;
        }

        currentState = State.Wander; // Caso contrário, continua em "Wander"
    }


    private void Wander()
    {
        // Define um destino aleatório
        gameObject.GetComponent<Ghost_Controller>().CreatePath();
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

    private void MoveToRandomNearbyPosition()
    {
        Vector3 randomDirection = transform.position + Random.insideUnitSphere * 5f; // Random direction within a 5-unit radius
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}

