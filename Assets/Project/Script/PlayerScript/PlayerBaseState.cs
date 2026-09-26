public abstract class PlayerBaseState : IState
{
    protected Player player;
    protected StateMachine stateMachine;

    public PlayerBaseState(Player player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}