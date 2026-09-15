using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementPureAddForce : MonoBehaviour
{
    [SerializeField] private float m_MoveForce = 20.0f;
    [SerializeField] private float m_JumpForce = 5.0f; 

    private Rigidbody2D m_Rigidbody;
    private InputAction m_MoveAction;
    private InputAction m_JumpAction;
    private float m_MoveInput;

    private void Awake()
    {
        m_MoveAction = InputSystem.actions.FindAction("Move");
        m_JumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_MoveInput = m_MoveAction.ReadValue<float>();

        if (m_JumpAction.WasPressedThisFrame())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        m_Rigidbody.AddForce(new Vector2(m_MoveInput * m_MoveForce, 0f), ForceMode2D.Force);
    }

    private void Jump()
    {
        m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
    }
}