using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 5.0f;
    [SerializeField] private Transform m_PosClosed;
    [SerializeField] private Transform m_PosOpen;

    private Vector3 m_TargetPosition;
    private bool m_IsOpen;

    private void Start()
    {
        if (m_PosClosed != null)
        {
            transform.position = m_PosClosed.position;
            m_TargetPosition = m_PosClosed.position;
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, m_MoveSpeed * Time.deltaTime);
    }

    public void ToggleDoor()
    {
        m_IsOpen = !m_IsOpen;
        m_TargetPosition = m_IsOpen ? m_PosOpen.position : m_PosClosed.position;
    }
}