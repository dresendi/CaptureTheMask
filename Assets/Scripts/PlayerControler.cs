using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    public float speed = 6f;
    public float acceleration = 20f;
    public float deceleration = 25f;
    private Animator animator;

    private Vector2 currentVelocity;

    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite maskedSprite;

    private SpriteRenderer spriteRenderer;
    private PlayerMaskHandler myMaskHandler;
    private bool lastMaskState;

    private Rigidbody2D rb;
    private GatherInput input;

    private bool isStunned;
    private float stunEndTime;
    private int IdSpeed;
    private int idHasMask ;
    private int idIsDead ;
    private bool isDead = false;

    public void setIsDead(bool value)
    {
        isDead = value;
        animator.SetBool(idIsDead, isDead);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<GatherInput>();
        animator = GetComponent<Animator>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myMaskHandler = GetComponent<PlayerMaskHandler>();
    }

    void Start()
    {
        IdSpeed = Animator.StringToHash("Speed");
        idHasMask = Animator.StringToHash("hasMask");
        idIsDead = Animator.StringToHash("isDead");
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

        // 🔄 FLIP DEL SPRITE SEGÚN DIRECCIÓN HORIZONTAL
        if (input.Movement.x < -0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (input.Movement.x > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void Update()
    {
        UpdateMaskVisual();
    }

    void UpdateMaskVisual()
    {
        if (MaskOwnershipManager.Instance == null || spriteRenderer == null || myMaskHandler == null)
            return;

        bool hasMaskNow = MaskOwnershipManager.Instance.CurrentOwner == myMaskHandler;

        if (hasMaskNow != lastMaskState)
        {
            lastMaskState = hasMaskNow;
            animator.SetBool(idHasMask , hasMaskNow);
            Debug.LogError("idHasMask : " + hasMaskNow);
            //spriteRenderer.sprite = hasMaskNow ? maskedSprite : normalSprite;
        }
    }
}
