using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerDash))]
[RequireComponent(typeof(PlayerCombat))]
public class Player : MonoBehaviour
{
    public PlayerMovement Movement { get; private set; }
    public PlayerHealth Health { get; private set; }
    public PlayerDash Dash { get; private set; }
    public PlayerCombat Combat { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    public InputAction MoveAction { get; private set; }
    public InputAction JumpAction { get; private set; }
    public InputAction ATKAction { get; private set; }
    public InputAction DashAction { get; private set; }

    public float MoveInput { get; private set; }
    public float OriginalGravity { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        Health = GetComponent<PlayerHealth>();
        Dash = GetComponent<PlayerDash>();
        Combat = GetComponent<PlayerCombat>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        OriginalGravity = Rigidbody.gravityScale;

        if (InputSystem.actions != null)
        {
            MoveAction = InputSystem.actions.FindAction("Move");
            JumpAction = InputSystem.actions.FindAction("Jump");
            ATKAction = InputSystem.actions.FindAction("ATK");
            DashAction = InputSystem.actions.FindAction("Dash");
        }

        StateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
        JumpState = new PlayerJumpState(this, StateMachine);
        DashState = new PlayerDashState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        if (MoveAction != null)
            MoveInput = MoveAction.ReadValue<float>();

        Movement.ProcessMovement(MoveInput);
        StateMachine.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    public void TakeDamage(float damage)
    {
        Health.TakeDamage(damage);
    }
}