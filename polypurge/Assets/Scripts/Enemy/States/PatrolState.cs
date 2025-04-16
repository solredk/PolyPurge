using UnityEngine;

public class PatrolState : BaseState
{
    public int wayPointIndex;
    public float waitTimer;
    public override void Enter()
    {

    }
    public override void Perform()
    {
        PatrolCicle();
        if (enemyBase.CanSeePlayer())
        {    
            if (enemyBase.enemyType == EnemyType.Ranged)
            {
                stateMachine.SwitchState(new Attackstate());
            }
            else if (enemyBase.enemyType == EnemyType.explosive)
            {
                stateMachine.SwitchState(new ExplosiveAttackState());
            }

        }
    }
    public override void Exit()
    {

    }
    public void PatrolCicle()
    {
        if (enemyBase.navMeshAgent.remainingDistance < 0.02f) 
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= 3)
            {
                if (wayPointIndex == enemyBase.path.waypoints.Count - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;
                enemyBase.navMeshAgent.SetDestination(enemyBase.path.waypoints[wayPointIndex].position);
                waitTimer = 0;
            }
        }
    }
}
