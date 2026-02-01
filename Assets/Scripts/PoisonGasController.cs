using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PoisonGasController : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private float growRate = 0.2f;
    [SerializeField] private float emissionIncrease = 5f;
    [SerializeField] private float checkInterval = 1f;
    [SerializeField] private Tilemap spawnTilemap;


    private ParticleSystem ps;
    private float currentRadius = 1f;
    private float maxRadius;
    private float timer;
    private CircleCollider2D gasCollider;



    void Awake()
    {
        ps = GetComponent<ParticleSystem>();

        Bounds bounds = map.localBounds;
        maxRadius = Mathf.Max(bounds.size.x, bounds.size.y) * 0.75f;
        gasCollider = GetComponent<CircleCollider2D>();
        gasCollider.radius = 1f;
        gasCollider.enabled = false;

    }

    public void ActivateGas(Vector3 startPos)
    {
        
        Debug.Log($"☣️ Activando gas en {startPos}");
        transform.position = startPos;
        gameObject.SetActive(true);

        gasCollider.enabled = true;
        ps.Play();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            timer = 0f;

            currentRadius += growRate;

            var shape = ps.shape;
            shape.radius = currentRadius;
            gasCollider.radius = currentRadius;


            var emission = ps.emission;
            emission.rateOverTime = emission.rateOverTime.constant + emissionIncrease;

            if (currentRadius >= maxRadius)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Debug.Log("☣️ Gas cubrió el mapa — Decidir ganador");

        var owner = MaskOwnershipManager.Instance.CurrentOwner;

        if (owner != null)
            Debug.Log($"🏆 Gana {owner.name} (tenía la máscara)");
        else
            Debug.Log("❌ Nadie tenía máscara — empate o fallo");

        enabled = false;
    }

    // 🔽 MÉTODO QUE FALTABA
    public Vector3 GetSpawnPointFarFromPlayers(Transform p1, Transform p2)
    {
        Bounds bounds = map.localBounds;

        Vector3[] corners = new Vector3[]
        {
            new Vector3(bounds.min.x, bounds.min.y, 0),
            new Vector3(bounds.min.x, bounds.max.y, 0),
            new Vector3(bounds.max.x, bounds.min.y, 0),
            new Vector3(bounds.max.x, bounds.max.y, 0)
        };

        Vector3 bestCorner = corners[0];
        float maxDist = 0f;

        foreach (var c in corners)
        {
            float dist = Vector2.Distance(c, p1.position) + Vector2.Distance(c, p2.position);
            if (dist > maxDist)
            {
                maxDist = dist;
                bestCorner = c;
            }
        }

        return bestCorner;
    }

    public Vector3 GetSpawnPointFromTilemapFarFromPlayers(Transform p1, Transform p2)
    {
        Debug.Log("🧠 Buscando celdas válidas en spawnTilemap...");
        List<Vector3> validSpawns = new List<Vector3>();

        foreach (var pos in spawnTilemap.cellBounds.allPositionsWithin)
        {
            if (!spawnTilemap.HasTile(pos)) continue;

            Vector3 worldPos = spawnTilemap.GetCellCenterWorld(pos);

            float dist = Vector2.Distance(worldPos, p1.position) +
                        Vector2.Distance(worldPos, p2.position);

            validSpawns.Add(worldPos + Vector3.forward * 0); // mantener Z
        }

        Debug.Log($"🧩 Celdas válidas encontradas: {validSpawns.Count}");

        // Elegir el más lejano de ambos jugadores
        Vector3 best = validSpawns[0];
        float maxDist = 0f;

        foreach (var p in validSpawns)
        {
            float d = Vector2.Distance(p, p1.position) + Vector2.Distance(p, p2.position);
            if (d > maxDist)
            {
                maxDist = d;
                best = p;
            }
        }

        return best;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"☣️ Algo entró al gas: {other.name}");

        PlayerMaskHandler player = other.GetComponent<PlayerMaskHandler>();
        if (player == null)
        {
            Debug.Log("❌ No tiene PlayerMaskHandler");
            return;
        }

        if (!player.HasMask)
        {
            Debug.Log($"☠️ {other.name} murió por gas");
            GameController.Instance.PlayerLost(player);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }


}
