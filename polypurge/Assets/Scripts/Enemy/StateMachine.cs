using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // the current state that is active 
    public BaseState currentState;



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
        SwitchState(new PatrolState());
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
