using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase,
        Attack
    }

    [Header("State")]
    [SerializeField] private State currentState = State.Patrol;

    [Header("Health Settings")]
    [SerializeField] private float m_MaxHealth = 100f;
    private float m_CurrentHealth;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float chaseSpeed = 4.5f;
    [SerializeField] private float rayDistance = 0.6f;
    [SerializeField] private LayerMask wallLayer;

    [Header("Detection & Attack Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float loseRange = 7f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1.5f;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private float lastAttackTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    private void Update()
    {
        CheckStateSwitch();

        if (currentState == State.Patrol)
        {
            CheckWall();
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Patrol:
                HandlePatrol();
                break;
            case State.Chase:
                HandleChase();
                break;
            case State.Attack:
                HandleAttack();
                break;
        }
    }

    private void CheckStateSwitch()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            currentState = State.Attack;
        }
        else if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chase;
        }
        else if (distanceToPlayer > loseRange)
        {
            currentState = State.Patrol;
        }
    }

    private void HandlePatrol()
    {
        float moveDirection = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
    }

    private void HandleChase()
    {
        if (player == null) return;

        LookAtPlayer();

        float moveDirection = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveDirection * chaseSpeed, rb.linearVelocity.y);
    }

    private void HandleAttack()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        LookAtPlayer();

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    private void PerformAttack()
    {
        if (player == null) return;

        Player playerHealth = player.GetComponent<Player>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log($"{gameObject.name} tấn công Player! Gây {attackDamage} sát thương.");
        }
    }

    private void LookAtPlayer()
    {
        if (player == null) return;

        float directionToPlayer = player.position.x - transform.position.x;

        if (directionToPlayer > 0 && !movingRight)
        {
            Flip();
        }
        else if (directionToPlayer < 0 && movingRight)
        {
            Flip();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        m_CurrentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} bị trúng đạn! -{damageAmount} HP. Máu còn lại: {m_CurrentHealth}/{m_MaxHealth}");

        if (m_CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} đã bị tiêu diệt!");
        Destroy(gameObject);
    }

    private void CheckWall()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, wallLayer);

        if (hit.collider != null)
        {
            Flip();
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        Gizmos.DrawLine(transform.position, transform.position + direction * rayDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, loseRange);
    }
}