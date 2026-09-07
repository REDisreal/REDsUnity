using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Singleton giúp gọi CoinManager từ bất kỳ đâu mà không cần kéo thả
    public static CoinManager Instance { get; private set; }

    [Header("Coin Data")]
    [SerializeField] private int m_CoinCount = 0;

    public int CoinCount => m_CoinCount;

    private void Awake()
    {
        // Khởi tạo Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin(int amount = 1)
    {
        m_CoinCount += amount;
        Debug.Log($"[CoinManager] Đã thu thập {amount} coin! Tổng cộng: {m_CoinCount}");
    }
}