using UnityEngine;
using UnityEngine.AI;
public class EnemyBase : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent navMeshAgent;
    public NavMeshAgent NavMeshAgent { get { return navMeshAgent; } }
    [SerializeField] private string currentState;

    private GameObject player;
    public float sightDistance;

    public float fieldOfView;

    public Path path;

    public float eyeHight;

    public bool kutbool;
    

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    private void Update()
    {
        kutbool = CanSeePlayer();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance) 
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray , out hitInfo, sightDistance))
                    {
                        Debug.Log("werk jij kanker ding");               
                        if (hitInfo.collider.gameObject == player)
                        {
                            return true;
                        }
                        Debug.DrawRay(ray.origin, ray.direction * sightDistance);  
                    }
                }
            }

        }
        return false;
    }
}
