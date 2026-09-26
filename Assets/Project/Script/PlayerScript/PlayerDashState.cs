using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private float m_DashTimer;

    public PlayerDashState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        m_DashTimer = player.Dash.DashDuration;

        player.Health.IsInvincible = true;
        player.Rigidbody.gravityScale = 0f;

        float direction = player.Movement.IsFacingRight ? 1f : -1f;
        player.Rigidbody.linearVelocity = new Vector2(direction * player.Dash.DashSpeed, 0f);

        DebugManager.Instance?.LogDash();

        player.Dash.StartCooldown();
    }

    public override void Update()
    {
        base.Update();

        m_DashTimer -= Time.deltaTime;
        if (m_DashTimer <= 0f)
        {
            if (player.Movement.IsGrounded)
                stateMachine.ChangeState(player.IdleState);
            else
                stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.Health.IsInvincible = false;
        player.Rigidbody.gravityScale = player.OriginalGravity;
    }
}