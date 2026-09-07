using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int m_CoinValue = 1; // Giá trị của đồng xu này (ví dụ: xu vàng = 1, xu kim cương = 5)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Cộng coin vào Manager
            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.AddCoin(m_CoinValue);
            }

            // Xóa đồng xu
            Destroy(gameObject);
        }
    }
}