public interface IState
{
    void Enter();
    void Update();
    void FixedUpdate();
    void Exit();
}

public class StateMachine
{
    public IState CurrentState { get; private set; }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}