using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float m_MaxHealth = 100f;
    private float m_CurrentHealth;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rayDistance = 0.6f; // Độ dài tia quét tường
    [SerializeField] private LayerMask wallLayer;      // Chọn Layer của Tường

    private Rigidbody2D rb;
    private bool movingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_CurrentHealth = m_MaxHealth; // Khởi tạo máu ban đầu
    }

    private void Update()
    {
        CheckWall();
    }

    private void FixedUpdate()
    {
        // Di chuyển quái sang trái hoặc phải
        float moveDirection = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
    }

    // Hàm nhận sát thương gọi từ Bullet.cs
    public void TakeDamage(float damageAmount)
    {
        m_CurrentHealth -= damageAmount;
        Debug.Log($"[SÁT THƯƠNG] {gameObject.name} bị trúng đạn! -{damageAmount} HP. Máu còn lại: {m_CurrentHealth}/{m_MaxHealth}");

        if (m_CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"[KẺ ĐỊCH GỤC NGÃ] {gameObject.name} đã bị tiêu diệt!");
        Destroy(gameObject);
    }

    private void CheckWall()
    {
        // Xác định hướng quét dựa trên hướng đi hiện tại
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;

        // Bắn một tia Raycast ra phía trước để kiểm tra tường
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, wallLayer);

        // Nếu đụng tường -> Quay đầu
        if (hit.collider != null)
        {
            Flip();
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;

        // Xoay Sprite bằng cách đổi dấu scale X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        Gizmos.DrawLine(transform.position, transform.position + direction * rayDistance);
    }
}