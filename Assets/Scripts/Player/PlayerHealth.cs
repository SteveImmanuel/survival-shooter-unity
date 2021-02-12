using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShooting))]
public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float damageFlashDuration = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    [HideInInspector]
    public static int currentHealth;
    private Animator animator;
    private AudioSource playerAudio;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private bool isDead;
    private bool damaged;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageFlashDuration * Time.deltaTime);
        }

        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        UIController.instance.SetHealthSlider(((float)currentHealth / (float)startingHealth) * 100);

        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);
        UIController.instance.SetHealthSlider(((float)currentHealth / (float)startingHealth) * 100);
    }


    private void Death()
    {
        playerAudio.clip = deathClip;
        playerAudio.Play();
        animator.SetTrigger("Dead");

        playerMovement.StopMoving();
        playerMovement.enabled = false;
        playerShooting.enabled = false;

        isDead = true;
    }

    public void OnAnimationEnd()
    {
        UIController.instance.GameOver();
    }
}
