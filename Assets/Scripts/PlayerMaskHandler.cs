using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMaskHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerControler controller;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerControler>();
    }

    public void ApplyKnockback(Vector2 force, float stunDuration)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);

        controller.ApplyStun(stunDuration);

        Debug.Log($"{name} Knockback aplicado: {force}");
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMaskHandler other = collision.collider.GetComponent<PlayerMaskHandler>();
        if (other == null) return;

        Debug.Log($"[COLLISION] {name} chocó con {other.name}");

        if (!HasMask && other.HasMask)
        {
            Debug.Log($"[STEAL ATTEMPT] {name} intenta robar a {other.name}");
            MaskOwnershipManager.Instance.TrySteal(this, other);
        }
    }

    public bool HasMask
    {
        get
        {
            bool result = MaskOwnershipManager.Instance != null &&
                          MaskOwnershipManager.Instance.CurrentOwner == this;

            Debug.Log($"[CHECK MASK] {name} HasMask = {result}");
            return result;
        }
    }
}

