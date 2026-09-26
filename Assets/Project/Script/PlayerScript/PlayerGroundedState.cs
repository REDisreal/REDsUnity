using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Update()
    {
        base.Update();

        if (player.DashAction != null && player.DashAction.WasPressedThisFrame() && player.Dash.CanDash)
        {
            stateMachine.ChangeState(player.DashState);
            return;
        }

        if (player.JumpAction != null && player.JumpAction.WasPressedThisFrame() && player.Movement.IsGrounded)
        {
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        if (player.ATKAction != null && player.ATKAction.WasPressedThisFrame())
        {
            player.Combat.TryShoot();
        }
    }
}