public abstract class BaseState
{
    public EnemyBase enemyBase;
    public StateMachine stateMachine;
    public abstract void Enter();

    public abstract void Perform();

    public abstract void Exit();
}
