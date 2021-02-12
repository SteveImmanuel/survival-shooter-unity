using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class IndicatorController : MonoBehaviour
{
    public float castRadius;
    public float timeBetweenCast;
    public GameObject arrow;
    public Image arrowIndicator;
    public Color arrowIdle;
    public Color powerUpActivated;
    public Text enemyDistanceText;
    public Text powerUpDurationText;
    public LayerMask detectableMask;

    private Quaternion originalRotation;
    private Animator animator;
    [HideInInspector]
    public static IndicatorController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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

    public void ActivatePowerUp()
    {
        arrowIndicator.color = powerUpActivated;
        powerUpDurationText.enabled = true;
    }

    public void SetPowerUpDurationText(float timeLeft)
    {
        powerUpDurationText.text = string.Format("{0:0.0} s", timeLeft);
    }

    public void DeactivatePowerUp()
    {
        arrowIndicator.color = arrowIdle;
        powerUpDurationText.enabled = false;
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
