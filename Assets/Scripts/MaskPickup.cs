using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    private bool alreadyTaken = false;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (alreadyTaken) return;

        PlayerMaskHandler player = other.GetComponent<PlayerMaskHandler>();
        if (player == null) return;

        if (MaskOwnershipManager.Instance == null)
        {
            Debug.LogError("[MASK PICKUP] No existe MaskOwnershipManager en la escena");
            return;
        }

        if (MaskOwnershipManager.Instance.HasOwner)
        {
            Debug.Log("[MASK PICKUP] Ya hay dueño, no se puede recoger");
            return;
        }

        alreadyTaken = true;

        Debug.Log($"[MASK PICKUP] {player.name} recogió la máscara");

        MaskOwnershipManager.Instance.AssignOwner(player);

        Destroy(gameObject);

    }
}
