using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    public event EventHandler<OnEnemySpawnedEventArgs> OnEnemySpawned;

    [SerializeField] List<GameObject> powerupList;
    [SerializeField] List<WaveData> waveDataList;

    Transform enemyContainer;

    bool isPlayerAlive;

    void Start()
    {
        Player player = FindObjectOfType<Player>();

        if (player != null)
        {
            isPlayerAlive = true;
            player.OnPlayerDied += Player_OnPlayerDeath;
        }

        enemyContainer = transform.Find("EnemyContainer");
        if (enemyContainer != null)
        {
            StartCoroutine(SpawnEnemyRoutine());
        }

        StartCoroutine(SpawnPowerupRoutine());

    }



    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1f);

        foreach (WaveData wave in waveDataList)
        {
            if(wave == null) 
            {
                Debug.Log("SpawnManager: WaveData item is null");
                continue; 
            }

            GameObject enemyPrefab = wave.GetEnemy();

            for (int i = 0; i < wave.GetEnemyTotal(); i++)
            {
               
                //EnemyMovement path = wave.GetEnemyMovementLogic();
                GameObject enemyObject = Instantiate(enemyPrefab, wave.GetSpawnPoint().position, Quaternion.identity);
                enemyObject.transform.parent = enemyContainer;
               
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                wave.GetEnemyMovementLogic().Movement(enemy);   // places movement routine on enemy
                OnEnemySpawned?.Invoke(this, new OnEnemySpawnedEventArgs { enemy = enemy });

                yield return new WaitForSeconds(wave.GetSpawnFrequency());
            }

            yield return new WaitForSeconds(wave.GetTimeToNextWave());

        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (isPlayerAlive)
        {
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-10.5f, 10.5f), 6, 0);

            int randomIndex = UnityEngine.Random.Range(0, powerupList.Count);
            GameObject newPowerup = Instantiate(powerupList[randomIndex], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(UnityEngine.Random.Range(8, 15));
        }

        yield return null;
    }

    void Player_OnPlayerDeath(object sender, EventArgs e)
    {
        isPlayerAlive = false;
    }
}

public class OnEnemySpawnedEventArgs : EventArgs
{
    public Enemy enemy;
}
