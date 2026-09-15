using UnityEngine;
using UnityEngine.InputSystem;

public class Moving : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 10.0f;
    [SerializeField] private Transform diemA;
    [SerializeField] private Transform diemB;
    private InputAction m_Interact;
    private Vector3 m_TargetPosition;
    private bool m_IsMovingTo;

    private InputAction m_MoveAction;
    private float m_MoveInput;

    private void Awake()
    {
        m_Interact = InputSystem.actions.FindAction("Interact");
    }

    private void Start()
    {
        if (diemA != null)
        {
            transform.position = diemA.position;
            m_TargetPosition = diemA.position;
        }
    }

    private void Update()
    {
        if (m_Interact.WasPressedThisFrame())
        {
            m_IsMovingTo = !m_IsMovingTo;
            m_TargetPosition = m_IsMovingTo ? diemB.position : diemA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, m_MoveSpeed * Time.deltaTime);
    }
}