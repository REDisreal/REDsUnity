using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float m_MaxHealth = 100f;
    [SerializeField] private float m_InvincibilityDuration = 1f;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_HurtSound;

    [Header("Events")]
    public UnityEvent<float, float> OnHealthChanged;
    public UnityEvent OnPlayerDied;

    private float m_CurrentHealth;
    private bool m_IsInvincible = false;

    public bool IsInvincible
    {
        get => m_IsInvincible;
        set => m_IsInvincible = value;
    }

    private void Awake()
    {
        if (m_SpriteRenderer == null) m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (m_AudioSource == null) m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        OnHealthChanged?.Invoke(m_CurrentHealth, m_MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (m_IsInvincible || m_CurrentHealth <= 0) return;

        m_CurrentHealth -= damage;
        m_CurrentHealth = Mathf.Max(m_CurrentHealth, 0f);

        DebugManager.Instance?.LogPlayerDamage(damage, m_CurrentHealth, m_MaxHealth);

        OnHealthChanged?.Invoke(m_CurrentHealth, m_MaxHealth);

        if (m_AudioSource != null && m_HurtSound != null)
        {
            m_AudioSource.PlayOneShot(m_HurtSound);
        }

        if (m_CurrentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }

    private void Die()
    {
        DebugManager.Instance?.LogPlayerDeath();
        OnPlayerDied?.Invoke();
        gameObject.SetActive(false);
    }

    private IEnumerator InvincibilityRoutine()
    {
        m_IsInvincible = true;

        float elapsed = 0f;
        float flashInterval = 0.1f;

        while (elapsed < m_InvincibilityDuration)
        {
            if (m_SpriteRenderer != null)
            {
                Color color = m_SpriteRenderer.color;
                color.a = color.a == 1f ? 0.3f : 1f;
                m_SpriteRenderer.color = color;
            }

            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        if (m_SpriteRenderer != null)
        {
            Color color = m_SpriteRenderer.color;
            color.a = 1f;
            m_SpriteRenderer.color = color;
        }

        m_IsInvincible = false;
    }
}