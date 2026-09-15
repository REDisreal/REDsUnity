using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementMovePosition : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float m_MoveSpeed = 10.0f;

    private Rigidbody2D m_Rigidbody;
    private InputAction m_MoveAction;
    private float m_MoveInput;

    private void Awake()
    {
        m_MoveAction = InputSystem.actions.FindAction("Move");
    }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_MoveInput = m_MoveAction.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = m_Rigidbody.position + new Vector2(m_MoveInput * m_MoveSpeed * Time.fixedDeltaTime, 0f);
        m_Rigidbody.MovePosition(targetPosition);
    }
}