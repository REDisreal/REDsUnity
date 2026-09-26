using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.Movement.TryJump();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // Cho phép di chuyển ngang khi đang nhảy/rơi
        player.Movement.Move(player.MoveInput);
    }

    public override void Update()
    {
        base.Update();

        // 1. Cho phép DASH trên không
        if (player.DashAction != null && player.DashAction.WasPressedThisFrame() && player.Dash.CanDash)
        {
            stateMachine.ChangeState(player.DashState);
            return;
        }

        // 2. CHO PHÉP BẮN ĐẠN TRÊN KHÔNG
        if (player.ATKAction != null && player.ATKAction.WasPressedThisFrame())
        {
            player.Combat.TryShoot();
        }

        // 3. Kiểm tra tiếp đất để về Idle hoặc Move
        if (player.Movement.IsGrounded && player.Rigidbody.linearVelocity.y <= 0.01f)
        {
            if (Mathf.Abs(player.MoveInput) > 0.01f)
                stateMachine.ChangeState(player.MoveState);
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }
}