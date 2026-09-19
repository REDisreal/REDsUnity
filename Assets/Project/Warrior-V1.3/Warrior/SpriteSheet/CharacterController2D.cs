using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Attack Settings")]
    [SerializeField] private float comboResetTime = 1.2f; // Thời gian tối đa để bấm đòn tiếp theo

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private float moveInput;
    private bool chamdat;
    private bool isFacingRight = true;

    private int combo = 0;
    private float lastAttackTime;

    private void Awake()
    {
        if (anim == null) anim = GetComponent<Animator>();
        if (rb == null) rb = GetComponentInParent<Rigidbody2D>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("ATK");
    }

    private void OnEnable()
    {
        moveAction?.Enable();
        jumpAction?.Enable();
        attackAction?.Enable();
    }

    private void OnDisable()
    {
        moveAction?.Disable();
        jumpAction?.Disable();
        attackAction?.Disable();
    }

    private void Update()
    {
        float rawInput = moveAction != null ? moveAction.ReadValue<float>() : 0f;

        if (Mathf.Abs(rawInput) > 0.1f)
        {
            moveInput = Mathf.Sign(rawInput);
        }
        else
        {
            moveInput = 0f;
        }

        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }

        chamdat = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (jumpAction != null && jumpAction.WasPressedThisFrame() && chamdat)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("nhay");
        }

        HandleAttack();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleAttack()
    {
        // Reset về 0 nếu người chơi dừng bấm quá lâu
        if (Time.time - lastAttackTime > comboResetTime)
        {
            combo = 0;
            anim.SetInteger("combo", 0);
        }

        if (attackAction != null && attackAction.WasPressedThisFrame())
        {
            combo++;
            if (combo > 3)
            {
                combo = 1;
            }

            anim.SetInteger("combo", combo);
            anim.SetTrigger("Tancong");

            lastAttackTime = Time.time;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void UpdateAnimator()
    {
        bool isMoving = moveInput != 0f;
        anim.SetBool("dichuyen", isMoving);

        bool isFalling = !chamdat && rb.linearVelocity.y < -0.1f;
        anim.SetBool("roi", isFalling);

        anim.SetBool("chamdat", chamdat);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}