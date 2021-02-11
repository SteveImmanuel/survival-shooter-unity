using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public Transform gunPoint;
    public LayerMask shootableMask;

    private Gun gun;
    private float elapsedTime = 0f;
    private Ray shootRay;
    private RaycastHit shootHit;

    private void Start()
    {
        gun = GetComponentInChildren<Gun>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (Input.GetButton("Fire1") && elapsedTime >= timeBetweenBullets)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        elapsedTime = 0f;

        shootRay.origin = gunPoint.position;
        shootRay.direction = gunPoint.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gun.ShowEffect(gunPoint.position, shootHit.point, Vector3.positiveInfinity);
        }
        else
        {
            gun.ShowEffect(gunPoint.position, Vector3.positiveInfinity, shootRay.direction);

        }
    }
}