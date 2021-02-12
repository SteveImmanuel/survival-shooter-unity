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
    public float maxTimeBetween = 3f;
    public float minTimeBetween = 1f;
    public float timeUntilMaxDifficult = 50f;
    public Transform[] spawnPoints;

    private float[] range;
    private float elapsedTime = 0f;
    private float spawnTime;

    private void Start()
    {
        spawnTime = maxTimeBetween;
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
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (spawnTime <= 0)
        {
            Spawn();
            spawnTime = Mathf.Lerp(maxTimeBetween, minTimeBetween, elapsedTime / timeUntilMaxDifficult);
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
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
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyTypes[enemyIndex].gameObject, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
