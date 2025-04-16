using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        enemyBase.navMeshAgent.SetDestination(enemyBase.LastKnownPlayerPosition);
    }
    public override void Perform()
    {
        if (enemyBase.CanSeePlayer())
        {
            stateMachine.SwitchState(new Attackstate());
        }
        if (enemyBase.navMeshAgent.remainingDistance < enemyBase.navMeshAgent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if (moveTimer >= Random.Range(3, 7))
            {
                enemyBase.navMeshAgent.SetDestination(enemyBase.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0f;
            }
            if (searchTimer >= 10f)
            {
                //change to patrol state
                stateMachine.SwitchState(new PatrolState());
            }
        }
        else
        {
            searchTimer += Time.deltaTime;
            if (searchTimer >= 5f)
            {
                //change to patrol state
                stateMachine.SwitchState(new PatrolState());
            }
        }
    }
    public override void Exit()
    {

    }
}
