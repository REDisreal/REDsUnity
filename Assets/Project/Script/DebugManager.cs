using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance { get; private set; }

    [Header("Debug Settings")]
    [SerializeField] private bool enableLogs = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- LOG HEALTH ---
    public void LogPlayerDamage(float damage, float currentHP, float maxHP)
    {
        if (!enableLogs) return;
        Debug.Log($"[PlayerHealth] <color=red>Player trúng đạn -{damage} HP!</color> Máu còn lại: {currentHP}/{maxHP}");
    }

    public void LogPlayerHeal(float amount, float currentHP, float maxHP)
    {
        if (!enableLogs) return;
        Debug.Log($"[PlayerHealth] <color=green>Player được hồi +{amount} HP!</color> Máu hiện tại: {currentHP}/{maxHP}");
    }

    public void LogPlayerDeath()
    {
        if (!enableLogs) return;
        Debug.Log("<color=red><b>[PlayerHealth] Player đã chết! Game Over.</b></color>");
    }

    // --- LOG COMBAT & AMMO ---
    public void LogShoot(int currentAmmo, int maxAmmo)
    {
        if (!enableLogs) return;
        Debug.Log($"[PlayerCombat] <color=yellow>Bắn đạn!</color> Đạn còn lại: {currentAmmo}/{maxAmmo}");
    }

    public void LogOutOfAmmo()
    {
        if (!enableLogs) return;
        Debug.LogWarning("[PlayerCombat] Hết đạn! Không thể bắn.");
    }

    public void LogRecoil(Vector2 force)
    {
        if (!enableLogs) return;
        Debug.Log($"[PlayerCombat] <color=orange>Recoil applied!</color> Force X: {force.x}");
    }

    // --- LOG DASH ---
    public void LogDash()
    {
        if (!enableLogs) return;
        Debug.Log("[PlayerDash] <color=cyan>Player thực hiện DASH!</color>");
    }
}