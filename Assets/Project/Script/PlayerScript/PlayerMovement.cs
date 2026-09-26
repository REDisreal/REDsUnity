using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float m_MoveSpeed = 15.0f;
    [SerializeField] private float m_JumpForce = 15.0f;
    [SerializeField] private int m_MaxJumps = 1;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private float m_GroundCheckRadius = 0.5f;
    [SerializeField] private LayerMask m_GroundLayer;

    private Rigidbody2D m_Rigidbody;
    private bool m_IsGrounded;
    private int m_JumpsRemaining;
    private bool m_IsFacingRight = true;

    // Biến chặn ghi đè vận tốc khi bị Recoil/Knockback
    private float m_KnockbackTimer = 0f;

    public bool IsFacingRight => m_IsFacingRight;
    public bool IsGrounded => m_IsGrounded;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_JumpsRemaining = m_MaxJumps;
    }

    private void Update()
    {
        // Giảm thời gian Knockback theo thời gian thực
        if (m_KnockbackTimer > 0f)
        {
            m_KnockbackTimer -= Time.deltaTime;
        }
    }

    public void ProcessMovement(float moveInput)
    {
        CheckGrounded();

        if (moveInput > 0 && !m_IsFacingRight) Flip();
        else if (moveInput < 0 && m_IsFacingRight) Flip();
    }

    public void Move(float moveInput)
    {
        // NẾU ĐANG TRONG THỜI GIAN RECOIL -> BỎ QUA GHI ĐÈ VẬN TỐC X
        if (m_KnockbackTimer > 0f) return;

        if (Mathf.Abs(moveInput) > 0.01f)
        {
            m_Rigidbody.linearVelocity = new Vector2(moveInput * m_MoveSpeed, m_Rigidbody.linearVelocity.y);
        }
        else
        {
            float newVelocityX = Mathf.Lerp(m_Rigidbody.linearVelocity.x, 0f, Time.fixedDeltaTime * 25.0f);
            m_Rigidbody.linearVelocity = new Vector2(newVelocityX, m_Rigidbody.linearVelocity.y);
        }
    }

    public void StopMove()
    {
        if (m_KnockbackTimer > 0f) return;
        m_Rigidbody.linearVelocity = new Vector2(0f, m_Rigidbody.linearVelocity.y);
    }

    // Hàm gọi khi bị giật lùi (Recoil)
    public void ApplyKnockback(Vector2 force, float duration = 0.15f)
    {
        m_KnockbackTimer = duration; // Chặn di chuyển trong 'duration' giây
        m_Rigidbody.linearVelocity = new Vector2(0f, m_Rigidbody.linearVelocity.y); // Reset vận tốc cũ
        m_Rigidbody.AddForce(force, ForceMode2D.Impulse); // Áp dụng lực giật
    }

    public void TryJump()
    {
        if (m_JumpsRemaining > 0)
        {
            m_Rigidbody.linearVelocity = new Vector2(m_Rigidbody.linearVelocity.x, 0f);
            m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
            m_JumpsRemaining--;
        }
    }

    private void CheckGrounded()
    {
        if (m_GroundCheck == null) return;

        bool wasGrounded = m_IsGrounded;
        Collider2D hit = Physics2D.OverlapCircle(m_GroundCheck.position, m_GroundCheckRadius, m_GroundLayer);
        m_IsGrounded = hit != null;

        if (m_IsGrounded)
        {
            m_JumpsRemaining = m_MaxJumps;
        }
        else if (wasGrounded && m_JumpsRemaining == m_MaxJumps)
        {
            m_JumpsRemaining = m_MaxJumps - 1;
        }
    }

    private void Flip()
    {
        m_IsFacingRight = !m_IsFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        if (m_GroundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(m_GroundCheck.position, m_GroundCheckRadius);
        }
    }
}