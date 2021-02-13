using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class BasePowerUp : MonoBehaviour
{
    public float duration = 5f;

    private Animator animator;
    private AudioSource audioSource;
    private GameObject mesh;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        mesh = transform.GetChild(0).gameObject;
    }

    public void Spawn()
    {
        mesh.SetActive(true);
        animator.ResetTrigger("Despawn");
        StopCoroutine(DisableSelf());
        StartCoroutine(DisableSelf());
    }

    IEnumerator DisableSelf()
    {
        yield return new WaitForSeconds(duration - 0.5f);
        animator.SetTrigger("Despawn");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(TriggerPowerUp(other.gameObject));
        }   
    }

    IEnumerator TriggerPowerUp(GameObject player)
    {
        audioSource.Play();
        mesh.SetActive(false);
        Execute(player);
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }

    protected virtual void Execute(GameObject player)
    {
    }
}
