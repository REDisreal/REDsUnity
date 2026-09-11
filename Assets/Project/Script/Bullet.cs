using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_Speed = 20.0f;
    [SerializeField] private float m_LifeTime = 3.0f;
    [SerializeField] private float m_Damage = 20.0f; // Sát thương của đạn (20 HP)

    private Vector2 m_Direction = Vector2.right;

    private void Start()
    {
        Destroy(gameObject, m_LifeTime);
    }

    private void Update()
    {
        transform.Translate(m_Direction * m_Speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        m_Direction = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem vật bị trúng có nằm ở Layer "Enemy" không
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Lấy component Enemy từ quái để gây sát thương
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(m_Damage);
            }

            // Hủy viên đạn ngay sau khi va chạm
            Destroy(gameObject);
        }
    }
}