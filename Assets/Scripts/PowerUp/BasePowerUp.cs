using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BasePowerUp : MonoBehaviour
{
    public float duration = 5f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Spawn()
    {
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
            StopCoroutine(DisableSelf());
            Execute(other.gameObject);
            gameObject.SetActive(false);
        }   
    }

    protected virtual void Execute(GameObject player)
    {
    }
}
