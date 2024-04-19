using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int enemyCount;
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public float spawnRange = 9f;
    public int waveNumber = 1;
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerUpPrefab, GenerateSpawn(), powerUpPrefab.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber += 1;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerUpPrefab, GenerateSpawn(), powerUpPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawn()
    {

        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnVec = new Vector3(spawnX, 0, spawnZ);
        return spawnVec;

    }
    void SpawnEnemyWave(int count)
    {
        for (int i = 0; i < count; i++)
        {

            Instantiate(enemyPrefab, GenerateSpawn(), enemyPrefab.transform.rotation);
        }
    }
}
