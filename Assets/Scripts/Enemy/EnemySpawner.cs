using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyType
    {
        public GameObject gameObject;
        public float chanceToSpawn;
    }

    [SerializeField]
    public EnemyType[] enemyTypes;
    public float firstDelay = 2f;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private float[] range;

    private void Start()
    {
        range = new float[enemyTypes.Length];
        for (int i = 0; i < range.Length; i++)
        {
            if (i == 0)
            {
                range[i] = enemyTypes[i].chanceToSpawn;
            }
            else
            {
                range[i] = enemyTypes[i].chanceToSpawn + range[i - 1];
            }
        }

        InvokeRepeating(nameof(Spawn), firstDelay, spawnTime);
    }

    private void Spawn()
    {

        if (PlayerHealth.currentHealth <= 0f)
        {
            return;
        }

        float enemyRandomRange = Random.Range(0, range[range.Length - 1]);
        int enemyIndex = range.Length - 1;

        for (int i = 0; i < range.Length; i++)
        {
            if (enemyRandomRange < range[i])
            {
                enemyIndex = i;
                break;
            }
        }
        Debug.Log(enemyTypes[enemyIndex].gameObject.name);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyTypes[enemyIndex].gameObject, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
