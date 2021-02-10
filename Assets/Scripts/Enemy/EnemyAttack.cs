using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    private Animator animator;
    private GameObject player;
    private PlayerHealth playerHealth;
    //private EnemyHealth enemyHealth;
    private bool playerInRange;
    private float timer;

    void Awake()
    {
        //enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)
        {
            Attack();
        }

        if (playerHealth.currentHealth <= 0)
        {
            animator.SetTrigger("PlayerDead");
        }
    }

    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && col.isTrigger == false)
        {
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
