using UnityEngine;

public class MaskOwnershipManager : MonoBehaviour
{
    public static MaskOwnershipManager Instance;

    [Header("Steal Settings")]
    [SerializeField] private float stealCooldown = 1.2f;
    [SerializeField] private float knockbackForce = 8f;
    [SerializeField] private float stunDuration = 0.4f;

    [Header("Debug")]
    [SerializeField] private PlayerMaskHandler currentOwner;

    private float lastStealTime;

    public PlayerMaskHandler CurrentOwner => currentOwner;
    public bool HasOwner => currentOwner != null;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AssignOwner(PlayerMaskHandler player)
    {
        Debug.Log($"[OWNER CHANGE] {currentOwner?.name ?? "NINGUNO"} → {player.name}");
        currentOwner = player;
        lastStealTime = Time.time;
    }

    public bool CanSteal() => Time.time >= lastStealTime + stealCooldown;

    public void TrySteal(PlayerMaskHandler thief, PlayerMaskHandler victim)
    {
        Debug.Log($"[STEAL ATTEMPT] {thief.name} → {victim.name}");

        if (currentOwner != victim)
        {
            Debug.LogWarning("[STEAL FAIL] Victim no es el dueño actual");
            return;
        }

        if (!CanSteal())
        {
            Debug.LogWarning("[STEAL FAIL] Cooldown activo");
            return;
        }

        lastStealTime = Time.time;
        AssignOwner(thief);

        Vector2 dir = (victim.transform.position - thief.transform.position).normalized;
        victim.ApplyKnockback(dir * knockbackForce, stunDuration);

        Debug.Log("[STEAL SUCCESS]");
    }
}
