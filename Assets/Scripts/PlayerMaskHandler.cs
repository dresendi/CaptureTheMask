using UnityEngine;

public class PlayerMaskHandler : MonoBehaviour
{
    public bool HasMask { get; private set; }
    public MaskController mask;

    public void SetHasMask(bool value)
    {
        HasMask = value;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMaskHandler other = collision.collider.GetComponent<PlayerMaskHandler>();
        if (other == null) return;

        if (!HasMask || other.HasMask) return;

        if (mask == null)
        {
            Debug.LogError($"{name} no tiene referencia a MaskController");
            return;
        }

        mask.DropAndRespawn();
    }

}
