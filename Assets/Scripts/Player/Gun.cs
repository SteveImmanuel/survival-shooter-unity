using System;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public GameObject bulletPrefab;
    public int bulletPoolSize;

    private ParticleSystem gunParticles;
    private Light gunLight;
    private float elapsedTime;
    private Ray shootRay;
    private RaycastHit shootHit;
    private AudioSource gunAudio;
    private float effectsDisplayTime = 0.2f;
    private Queue<Tuple<GameObject, Bullet>> bulletPool;


    void Awake()
    {
        gunParticles = GetComponentInChildren<ParticleSystem>();
        gunLight = GetComponentInChildren<Light>();
        gunAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        bulletPool = new Queue<Tuple<GameObject, Bullet>>();

        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bullet.SetActive(false);
            bulletPool.Enqueue(new Tuple<GameObject, Bullet>(bullet, bulletScript));
        }
    }

    private void SpawnBullet(Vector3 position, Vector3 destination)
    {
        Tuple<GameObject, Bullet> tuple = bulletPool.Dequeue();
        tuple.Item1.transform.position = position;
        tuple.Item2.SetDestination(destination);
        tuple.Item1.SetActive(true);
        bulletPool.Enqueue(tuple);
    }

    public void DisableEffects()
    {
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
            SpawnBullet(shootOrigin, shootOrigin + direction * range);
        }
        else
        {
            SpawnBullet(shootOrigin, shootHit);
        }

        Invoke(nameof(DisableEffects), effectsDisplayTime);
    }
}