using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class IndicatorController : MonoBehaviour
{
    public float castRadius;
    public float timeBetweenCast;
    public GameObject arrow;
    public Text enemyDistanceText;
    public LayerMask detectableMask;

    private Quaternion originalRotation;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        originalRotation = transform.rotation;
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetNearbyEnemy), 0, timeBetweenCast);
    }

    private void Update()
    {
        transform.rotation = originalRotation;
    }

    private void GetNearbyEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, castRadius, detectableMask);
        if (enemies.Length <= 0)
        {
            animator.SetBool("ShowIndicator", false);
            return;
        }

        animator.SetBool("ShowIndicator", true);
        float closestDistance = Mathf.Infinity;
        Vector3 targetDirection = Vector3.zero;

        foreach (Collider enemy in enemies)
        {
            Vector3 enemyPosition = enemy.transform.position;
            enemyPosition.y = 0;

            Vector3 direction = (enemyPosition - transform.position);
            float sqrDistance = direction.sqrMagnitude;
            
            if (sqrDistance < closestDistance)
            {
                closestDistance = sqrDistance;
                targetDirection = direction;
            }
        }

        arrow.transform.rotation = Quaternion.LookRotation(targetDirection);
        enemyDistanceText.text = string.Format("{0:0.00} m", closestDistance / 10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, castRadius);
    }
}
