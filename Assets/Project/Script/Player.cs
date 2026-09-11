using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float m_MoveSpeed = 15.0f;
    [SerializeField] private float m_JumpForce = 15.0f;
    [SerializeField] private int m_MaxJumps = 1;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private float m_GroundCheckRadius = 0.5f;
    [SerializeField] private LayerMask m_GroundLayer;

    [Header("Weapon Settings")]
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private Transform m_FirePoint;
    [SerializeField] private float m_FireRate = 2f;
    [SerializeField] private float m_ShootDelay = 0.6f; // Thời gian trễ trước khi đạn xuất hiện (0.6s)
    [SerializeField] private int m_MaxAmmo = 10;
    [SerializeField] private float m_RecoilForce = 10.0f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource m_AudioSource; // Component phát âm thanh
    [SerializeField] private AudioClip m_ShootSound;   // File âm thanh tiếng súng

    [Header("Events")]
    public UnityEvent<int, int> OnAmmoChanged; // Sự kiện gửi số đạn hiện tại / tối đa
    public UnityEvent OnOutOfAmmo;            // Sự kiện thông báo hết đạn

    private Rigidbody2D m_Rigidbody;
    private InputAction m_MoveAction;
    private InputAction m_JumpAction;
    private InputAction m_ATK;

    private float m_MoveInput;
    private bool m_IsGrounded;
    private int m_JumpsRemaining;
    private bool m_IsFacingRight = true;

    private float m_NextFireTime = 0f;
    private int m_CurrentAmmo;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();

        if (InputSystem.actions != null)
        {
            m_MoveAction = InputSystem.actions.FindAction("Move");
            m_JumpAction = InputSystem.actions.FindAction("Jump");
            m_ATK = InputSystem.actions.FindAction("ATK");
        }

        // Tự động tìm AudioSource nếu chưa được gán trên Inspector
        if (m_AudioSource == null)
        {
            m_AudioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        m_JumpsRemaining = m_MaxJumps;
        m_CurrentAmmo = m_MaxAmmo;

        // Báo số đạn ban đầu
        OnAmmoChanged?.Invoke(m_CurrentAmmo, m_MaxAmmo);
    }

    private void Update()
    {
        CheckGrounded();

        if (m_MoveAction != null)
            m_MoveInput = m_MoveAction.ReadValue<float>();

        if (m_MoveInput > 0 && !m_IsFacingRight) Flip();
        else if (m_MoveInput < 0 && m_IsFacingRight) Flip();

        if (m_JumpAction != null && m_JumpAction.WasPressedThisFrame() && m_JumpsRemaining > 0)
        {
            Jump();
        }

        if (m_ATK != null && m_ATK.WasPressedThisFrame())
        {
            if (Time.time >= m_NextFireTime)
            {
                if (m_CurrentAmmo > 0)
                {
                    StartCoroutine(ShootRoutine());
                }
                else
                {
                    OnOutOfAmmo?.Invoke();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(m_MoveInput) > 0.01f)
        {
            m_Rigidbody.linearVelocity = new Vector2(m_MoveInput * m_MoveSpeed, m_Rigidbody.linearVelocity.y);
        }
        else
        {
            float newVelocityX = Mathf.Lerp(m_Rigidbody.linearVelocity.x, 0f, Time.fixedDeltaTime * 25.0f);
            m_Rigidbody.linearVelocity = new Vector2(newVelocityX, m_Rigidbody.linearVelocity.y);
        }
    }

    private IEnumerator ShootRoutine()
    {
        if (m_BulletPrefab == null || m_FirePoint == null) yield break;

        // Cập nhật hồi chiêu và số đạn ngay lập tức
        m_NextFireTime = Time.time + m_FireRate + m_ShootDelay;
        m_CurrentAmmo--;
        OnAmmoChanged?.Invoke(m_CurrentAmmo, m_MaxAmmo);

        // 1. Phát âm thanh NGAY LẬP TỨC khi bấm nút (không delay)
        if (m_AudioSource != null && m_ShootSound != null)
        {
            m_AudioSource.PlayOneShot(m_ShootSound);
        }

        // 2. Tạm dừng 0.5s trước khi viên đạn thực sự xuất hiện
        yield return new WaitForSeconds(m_ShootDelay);

        // 3. Khởi tạo viên đạn tại vị trí FirePoint
        GameObject bulletObj = Instantiate(m_BulletPrefab, m_FirePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        Vector2 shootDirection = m_IsFacingRight ? Vector2.right : Vector2.left;

        if (bullet != null)
        {
            bullet.SetDirection(shootDirection);
        }

        // Tác dụng lực giật lùi cho nhân vật khi đạn được bắn ra
        m_Rigidbody.AddForce(-shootDirection * m_RecoilForce, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        if (m_GroundCheck == null) return;

        bool wasGrounded = m_IsGrounded;
        Collider2D hit = Physics2D.OverlapCircle(m_GroundCheck.position, m_GroundCheckRadius, m_GroundLayer);
        m_IsGrounded = hit != null;

        if (m_IsGrounded)
        {
            m_JumpsRemaining = m_MaxJumps;
        }
        else if (wasGrounded && m_JumpsRemaining == m_MaxJumps)
        {
            m_JumpsRemaining = m_MaxJumps - 1;
        }
    }

    private void Jump()
    {
        m_Rigidbody.linearVelocity = new Vector2(m_Rigidbody.linearVelocity.x, 0f);
        m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
        m_JumpsRemaining--;
    }

    private void Flip()
    {
        m_IsFacingRight = !m_IsFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        if (m_GroundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(m_GroundCheck.position, m_GroundCheckRadius);
        }
    }
}