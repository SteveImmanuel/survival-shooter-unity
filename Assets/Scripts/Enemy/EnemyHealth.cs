using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    [HideInInspector]
    public int currentHealth;
    private Animator animator;
    private AudioSource enemyAudio;
    private ParticleSystem[] particles;
    private CapsuleCollider capsuleCollider;
    private bool isDead;
    private bool isSinking;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        particles= GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }

        enemyAudio.Play();
        currentHealth -= amount;

        particles[0].transform.position = hitPoint;
        particles[0].Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        animator.SetTrigger("Dead");
        capsuleCollider.isTrigger = true;

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        particles[1].Play();

        UIController.instance.AddScore(scoreValue);
        isDead = true;
    }


    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 2f);
        isSinking = true;
    }
}
