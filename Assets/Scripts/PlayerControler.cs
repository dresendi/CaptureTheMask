using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private GatherInput input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<GatherInput>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input.Movement * speed;
    }
}
