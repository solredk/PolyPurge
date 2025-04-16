using UnityEngine;
using System.Collections;

public class ExplosiveAttackState : BaseState
{
    private float losePlayerTimer = 0f;

    [SerializeField] private float explodeDelay = 0.5f;
    [SerializeField] private Material material;

    private bool isExploding = false;
    private Color originalColor;

    public override void Enter()
    {
        losePlayerTimer = 0f;
        isExploding = false;
    }

    public override void Exit()
    {
        enemyBase.navMeshAgent.isStopped = false;
    }

    public override void Perform()
    {
        if (enemyBase.CanSeePlayer() || enemyBase.HasBeenHit)
        {
            losePlayerTimer = 0f;

            float distanceToPlayer = Vector3.Distance(enemyBase.transform.position, enemyBase.Player.transform.position);
            enemyBase.navMeshAgent.SetDestination(enemyBase.Player.transform.position);

            if (distanceToPlayer <= enemyBase.explosionRange && !isExploding)
            {
                enemyBase.navMeshAgent.isStopped = true;
                enemyBase.StartCoroutine(ExplodeAfterDelay());
                isExploding = true;
            }

            enemyBase.LastKnownPlayerPosition = enemyBase.Player.transform.position;
            enemyBase.HasBeenHit = false;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer >= 5f)
            {
                stateMachine.SwitchState(new SearchState());
            }
        }

        
        if (!isExploding && material != null)
        {
            material.color = Color.Lerp(originalColor, Color.red, Time.time * 0.1f); 
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {

        yield return new WaitForSeconds(explodeDelay);

        enemyBase.Explode();          



    }
}