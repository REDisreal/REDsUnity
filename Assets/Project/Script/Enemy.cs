using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase
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

    [Header("Detection Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float loseRange = 7f;

    private Rigidbody2D rb;
    private bool movingRight = true;

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
        }
    }

    private void CheckStateSwitch()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentState == State.Patrol && distanceToPlayer <= detectionRange)
        {
            currentState = State.Chase;
        }
        else if (currentState == State.Chase && distanceToPlayer > loseRange)
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

        float directionToPlayer = player.position.x - transform.position.x;

        if (directionToPlayer > 0 && !movingRight)
        {
            Flip();
        }
        else if (directionToPlayer < 0 && movingRight)
        {
            Flip();
        }

        float moveDirection = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveDirection * chaseSpeed, rb.linearVelocity.y);
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, loseRange);
    }
}