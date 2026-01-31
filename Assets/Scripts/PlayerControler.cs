using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private GatherInput input;

    private bool isStunned;
    private float stunEndTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<GatherInput>();
    }

    public void ApplyStun(float duration)
    {
        isStunned = true;
        stunEndTime = Time.time + duration;
    }

    void FixedUpdate()
    {
        if (isStunned)
        {
            if (Time.time >= stunEndTime)
            {
                isStunned = false;
            }
            else
            {
                return; 
            }
        }

        rb.linearVelocity = input.Movement * speed;
    }
}
