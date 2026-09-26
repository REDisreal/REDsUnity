using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.Animator?.SetBool("isIdle", true);
        
        // Đảm bảo đứng yên hẳn khi vào Idle
        player.Movement.StopMove(); 
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        // Giữ đứng yên nếu đang ở Idle
        player.Movement.StopMove();
    }

    public override void Update()
    {
        base.Update();

        if (Mathf.Abs(player.MoveInput) > 0.01f)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }
}

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.Animator?.SetBool("isIdle", false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.Movement.Move(player.MoveInput);
    }

    public override void Update()
    {
        base.Update();

        // Nếu ngưng bấm phím (MoveInput gần bằng 0) thì về Idle
        if (Mathf.Abs(player.MoveInput) < 0.01f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        // Dừng di chuyển ngay lập tức khi thoát khỏi trạng thái di chuyển
        player.Movement.StopMove(); 
    }
}