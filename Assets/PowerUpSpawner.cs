using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct PowerUpType
    {
        public GameObject prefab;
        public int quantity;
    }

    [SerializeField]
    public PowerUpType[] powerUpTypes;
    public Transform[] spawnPoints;
    public float timeBetweenSpawn = 5f;

    private Queue<GameObject> powerUps;

    private void Start()
    {
        powerUps = new Queue<GameObject>();

        foreach (PowerUpType powerUpType in powerUpTypes)
        {
            for (int i = 0; i < powerUpType.quantity; i++)
            {
                GameObject powerUp = Instantiate(powerUpType.prefab);
                powerUp.SetActive(false);
                powerUps.Enqueue(powerUp);
            }
        }

        InvokeRepeating(nameof(SpawnPowerUp), 0, timeBetweenSpawn);
    }

    private void SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject powerUp = powerUps.Dequeue();
        BasePowerUp powerUpScript = powerUp.GetComponent<BasePowerUp>();
        powerUp.transform.position = position;
        powerUp.transform.rotation = rotation;
        powerUp.SetActive(true);
        powerUpScript.Spawn();
        powerUps.Enqueue(powerUp);
    }

    private void SpawnPowerUp()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        SpawnFromPool(spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
