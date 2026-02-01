using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    public float speed = 6f;
    public float acceleration = 20f;
    public float deceleration = 25f;
    private Animator animator;

    private Vector2 currentVelocity;


    private Rigidbody2D rb;
    private GatherInput input;

    private bool isStunned;
    private float stunEndTime;
    private int IdSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<GatherInput>();
        animator = GetComponent<Animator>();

    }

    void Start()
    {
        IdSpeed = Animator.StringToHash("Speed");
    }
    public void ApplyStun(float duration)
    {
        isStunned = true;
        stunEndTime = Time.time + duration;
    }

    void FixedUpdate()
    {

        float speedValue = rb.linearVelocity.magnitude;
        animator.SetFloat("speed", speedValue);


        if (isStunned)
        {
            if (Time.time >= stunEndTime)
                isStunned = false;
            else
                return;
        }

        Vector2 targetVelocity = input.Movement * speed;

        float accelRate = (targetVelocity.magnitude > 0.1f) ? acceleration : deceleration;

        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            targetVelocity,
            accelRate * Time.fixedDeltaTime
        );

        rb.linearVelocity = currentVelocity;
    }



}
