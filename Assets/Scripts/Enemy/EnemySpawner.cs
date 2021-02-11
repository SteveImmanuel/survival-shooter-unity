using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy;
    public float firstDelay = 2f;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), firstDelay, spawnTime);
    }

    private void Spawn()
    {

        if (PlayerHealth.currentHealth <= 0f)
        {
            return;
        }

        int enemyIndex = Random.Range(0, enemy.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
