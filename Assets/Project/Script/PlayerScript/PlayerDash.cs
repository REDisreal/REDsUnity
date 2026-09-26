using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float m_DashSpeed = 30.0f;
    [SerializeField] private float m_DashDuration = 0.2f;
    [SerializeField] private float m_DashCooldown = 1.0f;

    private bool m_CanDash = true;

    public float DashSpeed => m_DashSpeed;
    public float DashDuration => m_DashDuration;
    public bool CanDash => m_CanDash;

    public void StartCooldown()
    {
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        m_CanDash = false;
        yield return new WaitForSeconds(m_DashCooldown + m_DashDuration);
        m_CanDash = true;
    }
}