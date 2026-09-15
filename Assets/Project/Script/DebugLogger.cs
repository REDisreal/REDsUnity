using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    // Chỉ nhận và in log số đạn
    public void LogAmmo(int currentAmmo, int maxAmmo)
    {
        Debug.Log($"Số đạn hiện tại: {currentAmmo} / {maxAmmo}");
    }

    // In log cảnh báo khi hết đạn
    public void LogOutOfAmmo()
    {
        Debug.LogWarning("Đã hết đạn!");
    }
}