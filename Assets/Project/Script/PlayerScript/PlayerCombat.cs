using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private Weapon m_Weapon;
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private Transform m_FirePoint;
    [SerializeField] private float m_FireRate = 0.5f;
    [SerializeField] private float m_ShootDelay = 0.1f;
    [SerializeField] private int m_MaxAmmo = 10;

    [Header("Recoil Settings")]
    [SerializeField] private float m_RecoilForce = 25.0f; // Lực giật lùi về phía sau

    [Header("Audio Settings")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_ShootSound;

    [Header("Events")]
    public UnityEvent<int, int> OnAmmoChanged;
    public UnityEvent OnOutOfAmmo;

    private Rigidbody2D m_Rigidbody;
    private PlayerMovement m_Movement;
    private float m_NextFireTime = 0f;
    private int m_CurrentAmmo;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Movement = GetComponent<PlayerMovement>();
        if (m_AudioSource == null) m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_CurrentAmmo = m_MaxAmmo;
        OnAmmoChanged?.Invoke(m_CurrentAmmo, m_MaxAmmo);
    }

    public void TryShoot()
    {
        if (Time.time >= m_NextFireTime)
        {
            if (m_CurrentAmmo > 0)
            {
                StartCoroutine(ShootRoutine());
            }
            else
            {
                DebugManager.Instance?.LogOutOfAmmo();
                OnOutOfAmmo?.Invoke();
            }
        }
    }

    private IEnumerator ShootRoutine()
    {
        if (m_BulletPrefab == null || m_FirePoint == null) yield break;

        m_NextFireTime = Time.time + m_FireRate + m_ShootDelay;
        m_CurrentAmmo--;

        DebugManager.Instance?.LogShoot(m_CurrentAmmo, m_MaxAmmo);

        OnAmmoChanged?.Invoke(m_CurrentAmmo, m_MaxAmmo);

        if (m_Weapon != null) m_Weapon.TriggerWeapon();

        if (m_AudioSource != null && m_ShootSound != null)
        {
            m_AudioSource.PlayOneShot(m_ShootSound);
        }

        yield return new WaitForSeconds(m_ShootDelay);

        GameObject bulletObj = Instantiate(m_BulletPrefab, m_FirePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        Vector2 shootDirection = m_Movement.IsFacingRight ? Vector2.right : Vector2.left;

        if (bullet != null)
        {
            bullet.SetDirection(shootDirection);
        }

        ApplyHorizontalRecoil(shootDirection);
    }

    private void ApplyHorizontalRecoil(Vector2 shootDirection)
    {
        if (m_Movement == null) return;

        // Tính toán lực giật lùi ngược hướng bắn
        float recoilDirection = -shootDirection.x;
        Vector2 recoilVector = new Vector2(recoilDirection * m_RecoilForce, 0f);

        // Gọi ApplyKnockback trên Movement để chặn di chuyển trong 0.15 giây
        m_Movement.ApplyKnockback(recoilVector, 0.15f);
    }
}