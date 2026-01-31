using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (MaskGameManager.Instance.MaskIsOwned) return;

        PlayerMaskHandler player = other.GetComponent<PlayerMaskHandler>();
        if (player == null) return;

        MaskGameManager.Instance.AssignMask(player);

        Destroy(gameObject); // la máscara desaparece
    }
}
