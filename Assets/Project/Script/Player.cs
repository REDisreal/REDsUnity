using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float m_MoveSpeed = 15.0f;
    [SerializeField] private float m_JumpForce = 15.0f;
    [Header("Ground Check Settings")]
    [SerializeField] private Transform m_GroundCheck;      // Vị trí chân nhân vật
    [SerializeField] private float m_GroundCheckDistance = 0.2f; // Độ dài tia Raycast
    [SerializeField] private LayerMask m_GroundLayer;      // Layer được tính là mặt đất
    private Rigidbody2D m_Rigidbody;
    private InputAction m_MoveAction;
    private InputAction m_JumpAction;
    private InputAction m_ATK;
    private float m_MoveInput;
    private bool m_IsGrounded;

    private void Awake()
    {
        m_MoveAction = InputSystem.actions.FindAction("Move");
        m_JumpAction = InputSystem.actions.FindAction("Jump");
        m_ATK = InputSystem.actions.FindAction("ATK");
    }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGrounded();

        m_MoveInput = m_MoveAction.ReadValue<float>();

        if (m_JumpAction.WasPressedThisFrame() && m_IsGrounded)
        {
            Jump();
        }
        if (m_ATK.WasPressedThisFrame())
        {
            Debug.Log("Attack");
        }
    }

    private void FixedUpdate()
    {
        m_Rigidbody.linearVelocity = new Vector2(m_MoveInput * m_MoveSpeed, m_Rigidbody.linearVelocity.y);
    }
    private void CheckGrounded()
    {
        // Bắn 1 tia Raycast từ vị trí m_GroundCheck hướng thẳng xuống dưới (Vector2.down)
        RaycastHit2D hit = Physics2D.Raycast(m_GroundCheck.position, Vector2.down, m_GroundCheckDistance, m_GroundLayer);

        // Nếu tia Raycast va chạm với collider thuộc m_GroundLayer thì m_IsGrounded = true
        m_IsGrounded = hit.collider != null;
    }
    private void Jump()
    {
        m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
    }
    private void OnDrawGizmosSelected()
    {
        if (m_GroundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(m_GroundCheck.position, m_GroundCheck.position + Vector3.down * m_GroundCheckDistance);
        }
    }
}