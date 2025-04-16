using UnityEngine;
using UnityEngine.AI;
    public enum EnemyType
    {
        Ranged,
        explosive
    }

public class EnemyBase : MonoBehaviour
{


    public EnemyType enemyType;

    private StateMachine stateMachine;

    public GameObject Player;

    public Vector3 LastKnownPlayerPosition;


    [Header("AI")]
    public NavMeshAgent navMeshAgent;

    public bool HasBeenHit;

    [SerializeField] private GameManager gameManager;

    public Path path;

    [SerializeField] private LayerMask playermask;
    
    [Header("sight values")]
    public float sightDistance;
    public float fieldOfView;

    public bool CanSee;

    [SerializeField] private string currentState;

    public float eyeHight;

    [Header("weapon values")]
    public Transform gunBarrel;
    
    public GameObject bulletPrefab;

    [Range(0.1f,10f)]
    public  float FireRate;

    [Header("explosive specs")]

    [SerializeField] private GameObject explosion;
    [SerializeField] private float explosionDamage = 10f;

    public  float explosionRange = 3f;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        Player = GameObject.FindGameObjectWithTag("Player"); 
    }

    private void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.currentState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (Player != null)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) < sightDistance) 
            {
                Vector3 targetDirection = Player.transform.position - transform.position - (Vector3.up * eyeHight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    int layerMask = LayerMask.GetMask("Default", "Player"); // Voeg alle relevante lagen toe

                    if (Physics.Raycast(ray , out hitInfo, sightDistance, layerMask))
                    {      
                        if (hitInfo.collider.gameObject == Player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);  
                            return true;
                        }
                        Debug.Log(hitInfo.collider.gameObject.name);
                    }
                }
            }
        }
        return false;
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        if (Vector3.Distance(Player.transform.position, transform.position) <= explosionRange)
        {
            PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (gameManager != null)
                    gameManager.AddKill(1);
                playerHealth.TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
    }
    public void AlertAndAttackPlayer()
    {
        if (Player != null)
        {
            HasBeenHit = true;
            LastKnownPlayerPosition = Player.transform.position;
            if(enemyType == EnemyType.Ranged)
                stateMachine.SwitchState(new Attackstate());
            else if (enemyType == EnemyType.explosive)
                stateMachine.SwitchState(new ExplosiveAttackState()); 
        }
    }
}
