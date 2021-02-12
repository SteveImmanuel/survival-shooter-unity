using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float maxTimeToLive = 1f;

    private Vector3 destination;
    private Vector3 direction;
    private float elapsedTime;

    public void SetDestination(Vector3 destination)
    {
        elapsedTime = 0;
        this.destination = destination;
        direction = (destination - transform.position).normalized;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (destination != null && (destination - transform.position).sqrMagnitude > 1 && elapsedTime < maxTimeToLive)
        {
            transform.position = transform.position + direction * speed * Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
