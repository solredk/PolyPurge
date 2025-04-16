using UnityEngine;

public class Attackstate : BaseState
{
    private float moveTimer;
    [SerializeField]private float losePlayerTimer = 2f;

    public float shotTimer;

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if (enemyBase.CanSeePlayer() || enemyBase.HasBeenHit)
        {
            losePlayerTimer = 0f;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemyBase.transform.LookAt(enemyBase.Player.transform);
            if (shotTimer >= enemyBase.FireRate)
            {
                Shoot();
            }
            if (moveTimer >= Random.Range(3,7))   
            {
                enemyBase.navMeshAgent.SetDestination(enemyBase.transform.position +(Random.insideUnitSphere*5));
                moveTimer = 0f;
            }
            enemyBase.LastKnownPlayerPosition = enemyBase.Player.transform.position;
            enemyBase.HasBeenHit = false;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer >= 8f)
            {
                //change to search state
                stateMachine.SwitchState(new SearchState());
            }
        }

    }

    public void Shoot()
    {
        Transform gunbarrel = enemyBase.gunBarrel;
        GameObject bullet = enemyBase.bulletPrefab;
        Debug.Log("Shoot");
        Vector3 direction = (enemyBase.Player.transform.position - gunbarrel.position).normalized;
        GameObject.Instantiate(bullet, gunbarrel.position, enemyBase.transform.rotation);
        shotTimer = 0f;
    }


}
