using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTransform : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 10.0f;

    private InputAction m_MoveAction;
    private float m_MoveInput;

    private void Awake()
    {
        m_MoveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        m_MoveInput = m_MoveAction.ReadValue<float>();
        transform.Translate(Vector3.right * m_MoveInput * m_MoveSpeed * Time.deltaTime);
    }
}