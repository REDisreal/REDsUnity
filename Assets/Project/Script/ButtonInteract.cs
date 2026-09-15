using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonInteract : MonoBehaviour
{
    [SerializeField] private DoorMovement m_Door;

    private InputAction m_Interact;

    private void Awake()
    {
        m_Interact = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        if (m_Interact != null && m_Interact.WasPressedThisFrame())
        {
            PressButton();
        }
    }

    private void PressButton()
    {
        if (m_Door != null)
        {
            m_Door.ToggleDoor();
        }
    }
}