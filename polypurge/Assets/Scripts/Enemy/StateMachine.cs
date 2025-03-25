using UnityEngine;
using UnityEngine.XR;

public class StateMachine : MonoBehaviour
{
    // the current state that is active 
    private BaseState currentState;
    public PatrolState patrolState;

    // Start wordt aangeroepen bij het begin
    void Start()
    {
        
    }
    // Update wordt elke frame aangeroepen
    void Update()
    {
        if (currentState != null)
        {
            currentState.Perform();
        }
    }

    public void Initialise()
    {
        patrolState = new PatrolState();
        SwitchState(patrolState);
    }

    public void SwitchState(BaseState state)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.stateMachine = this;
            currentState.enemyBase = GetComponent<EnemyBase>();
            currentState.Enter();
        }

    }
}
