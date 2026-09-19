using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Moving : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 10.0f;
    [SerializeField] private Transform diemA;
    [SerializeField] private Transform diemB;

    private InputAction m_Interact;
    private bool m_IsMovingTo;
    private Coroutine m_MoveCoroutine;

    private void Awake()
    {
        m_Interact = InputSystem.actions.FindAction("Interact");
    }

    private void OnEnable()
    {
        if (m_Interact != null)
        {
            m_Interact.Enable();
            m_Interact.performed += OnInteractPerformed;
        }
    }

    private void OnDisable()
    {
        if (m_Interact != null)
        {
            m_Interact.performed -= OnInteractPerformed;
            m_Interact.Disable();
        }
    }

    private void Start()
    {
        if (diemA != null)
        {
            transform.position = diemA.position;
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        m_IsMovingTo = !m_IsMovingTo;
        Vector3 targetPosition = m_IsMovingTo ? diemB.position : diemA.position;

        if (m_MoveCoroutine != null)
        {
            StopCoroutine(m_MoveCoroutine);
        }

        m_MoveCoroutine = StartCoroutine(MoveToPosition(targetPosition));
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, m_MoveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
    }
}