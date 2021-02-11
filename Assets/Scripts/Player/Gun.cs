using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    private ParticleSystem gunParticles;
    private LineRenderer bulletTrail;
    private Light gunLight;
    private float elapsedTime;
    private Ray shootRay;
    private RaycastHit shootHit;
    private AudioSource gunAudio;
    private float effectsDisplayTime = 0.2f;

    void Awake()
    {
        gunParticles = GetComponentInChildren<ParticleSystem>();
        bulletTrail = GetComponentInChildren<LineRenderer>();
        gunLight = GetComponentInChildren<Light>();
        gunAudio = GetComponent<AudioSource>();
    }

    public void DisableEffects()
    {
        bulletTrail.enabled = false;
        gunLight.enabled = false;
    }

    public void ShowEffect(Vector3 shootOrigin, Vector3 shootHit, Vector3 direction)
    {
        gunAudio.Play();
        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        if (shootHit == Vector3.positiveInfinity)
        {
            bulletTrail.SetPosition(1, shootOrigin + direction * range);
        }
        else
        {
            bulletTrail.SetPosition(1, shootHit);
        }
        bulletTrail.SetPosition(0, shootOrigin);
        bulletTrail.enabled = true;

        Invoke(nameof(DisableEffects), effectsDisplayTime);
    }
}