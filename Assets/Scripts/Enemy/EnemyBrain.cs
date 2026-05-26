using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private enum EnemyState { Patrol, Chase, Attack }

    public EnemyMovement movement;
    public EnemyCombat combat;
    public EnemyStats enemyStats;
    public Transform playerTransform;

    private const float DetectionRadiusSquared = 64f;
    private const float AttackRadiusSquared = 4f;

    private EnemyState currentState;
    private bool isDead;

    private void Start()
    {
        currentState = EnemyState.Patrol;
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
            movement.Patrol();
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

    private void HandleDied()
    {
        isDead = true;
        movement.Stop();
    }
}
