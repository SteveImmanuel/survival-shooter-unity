using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    private Animator animator;
    private EnemyHealth enemyHealth;
    private GameObject player;
    private PlayerHealth playerHealth;
    private bool playerInRange = false;
    private float elapsedTime = 0f;

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (PlayerHealth.currentHealth <= 0)
        {
            animator.SetTrigger("PlayerDead");
        }
    }

    void Attack()
    {
        elapsedTime = 0f;

        if (PlayerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && col.isTrigger == false)
        {
            if (player == null)
            {
                player = col.gameObject;
                playerHealth = player.GetComponent<PlayerHealth>();
            }
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
