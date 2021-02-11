using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private EnemyHealth enemyHealth;
    private NavMeshAgent nav;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (enemyHealth.currentHealth > 0 && PlayerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.transform.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
