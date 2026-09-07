using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementLinearVelocity : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float m_MoveSpeed = 10.0f;
    [SerializeField] private float m_JumpForce = 7.0f; 

    private Rigidbody2D m_Rigidbody;
    private InputAction m_MoveAction;
    private InputAction m_JumpAction;

    private float m_MoveInput;
    private bool m_JumpRequested;

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
            m_JumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 currentVelocity = m_Rigidbody.linearVelocity;

        currentVelocity.x = m_MoveInput * m_MoveSpeed;

        if (m_JumpRequested)
        {
            currentVelocity.y = m_JumpForce; 
            m_JumpRequested = false;         
        }

        m_Rigidbody.linearVelocity = currentVelocity;
    }
}