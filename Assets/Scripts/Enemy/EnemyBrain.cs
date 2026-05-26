using UnityEngine;

public enum EnemyState { Patrol, Chase, Attack }

public class EnemyBrain : MonoBehaviour
{
    public EnemyMovement movement;
    public EnemyCombat combat;
    public EnemyStats enemyStats;
    public Transform playerTransform;
    public Transform patrolPointA;
    public Transform patrolPointB;

    public EnemyState currentState;

    private const float DetectionRadiusSquared = 64f;
    private const float AttackRadiusSquared = 4f;
    private const float PatrolPointReachThresholdSquared = 0.25f;

    private Transform currentPatrolTarget;
    private bool isDead;

    private void Start()
    {
        currentState = EnemyState.Patrol;
        currentPatrolTarget = patrolPointA;
        enemyStats.OnDied += HandleDied;
    }

    private void Update()
    {
        if (isDead || playerTransform == null)
        {
            return;
        }

        float distanceSquared = (playerTransform.position - transform.position).sqrMagnitude;
        TransitionState(distanceSquared);
        ExecuteCurrentState();
    }

    private void TransitionState(float distanceSquared)
    {
        if (currentState == EnemyState.Patrol && distanceSquared <= DetectionRadiusSquared)
        {
            currentState = EnemyState.Chase;
        }
        else if (currentState == EnemyState.Chase && distanceSquared <= AttackRadiusSquared)
        {
            currentState = EnemyState.Attack;
        }
        else if (currentState == EnemyState.Attack && distanceSquared > AttackRadiusSquared)
        {
            currentState = EnemyState.Chase;
        }
        else if (currentState == EnemyState.Chase && distanceSquared > DetectionRadiusSquared)
        {
            currentState = EnemyState.Patrol;
        }
    }

    private void ExecuteCurrentState()
    {
        if (currentState == EnemyState.Patrol)
        {
            AdvancePatrol();
        }
        else if (currentState == EnemyState.Chase)
        {
            movement.MoveTo(playerTransform.position);
        }
        else if (currentState == EnemyState.Attack)
        {
            movement.Stop();
            combat.TryAttack();
        }
    }

    private void AdvancePatrol()
    {
        movement.PatrolTo(currentPatrolTarget.position);

        Vector2 toTarget = currentPatrolTarget.position - transform.position;
        if (toTarget.sqrMagnitude < PatrolPointReachThresholdSquared)
        {
            if (currentPatrolTarget == patrolPointA)
            {
                currentPatrolTarget = patrolPointB;
            }
            else
            {
                currentPatrolTarget = patrolPointA;
            }
        }
    }

    private void HandleDied()
    {
        isDead = true;
        movement.Stop();
    }
}
