using UnityEngine;
using UnityEngine.Tilemaps;

public class MaskController : MonoBehaviour
{
    public PlayerMaskHandler currentOwner;

    [Header("Respawn Area")]
    public Tilemap respawnTilemap;

    private Collider2D col;
    private SpriteRenderer sr;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        if (respawnTilemap == null)
        {
            Debug.LogError("MaskController: respawnTilemap no asignado");
        }
    }

    public bool IsTaken => currentOwner != null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsTaken) return;

        PlayerMaskHandler player = other.GetComponent<PlayerMaskHandler>();
        if (player == null) return;

        Take(player);
    }

    public void Take(PlayerMaskHandler player)
    {
        currentOwner = player;
        player.SetHasMask(true);

        col.enabled = false;
        sr.enabled = false;
    }

    public void DropAndRespawn()
    {
        if (currentOwner == null || respawnTilemap == null) return;

        currentOwner.SetHasMask(false);
        currentOwner = null;

        Bounds bounds = respawnTilemap.localBounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(x, y, transform.position.z);

        col.enabled = true;
        sr.enabled = true;
    }

    void OnDrawGizmosSelected()
    {
        if (respawnTilemap == null) return;

        Gizmos.color = Color.green;
        Bounds b = respawnTilemap.localBounds;
        Gizmos.DrawWireCube(b.center, b.size);
    }

}
