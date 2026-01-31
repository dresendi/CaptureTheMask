using UnityEngine;

public class MaskGameManager : MonoBehaviour
{
    public static MaskGameManager Instance;

    public GameObject maskPrefab;
    public float respawnDistance = 6f;

    public PlayerMaskHandler currentOwner;

    void Awake()
    {
    if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }

    Instance = this;

    if (maskPrefab == null)
    {
        Debug.LogError("MaskGameManager: maskPrefab NO está asignado en el Inspector.");
    }
    }

    public bool MaskIsOwned => currentOwner != null;

    public void AssignMask(PlayerMaskHandler player)
    {
        currentOwner = player;
        player.SetHasMask(true);
    }

    public void RemoveMask(PlayerMaskHandler player)
    {
        if (currentOwner != player) return;

        player.SetHasMask(false);
        currentOwner = null;
    }

    public void RespawnMaskAwayFrom(Vector2 a, Vector2 b)
    {
        if (maskPrefab == null)
        {
            Debug.LogError("No se puede respawnear la máscara: maskPrefab es NULL");
            return;
        }

        Vector2 center = (a + b) * 0.5f;
        Vector2 dir = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = center + dir * respawnDistance;

        Instantiate(maskPrefab, spawnPos, Quaternion.identity);
    }

}
