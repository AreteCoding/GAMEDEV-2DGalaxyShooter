using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    public event EventHandler<OnEnemySpawnedEventArgs> OnEnemySpawned;

    [SerializeField] List<GameObject> powerupList;
    [SerializeField] GameObject pfEnemy;

    Transform enemyContainer;

    bool isPlayerAlive;

    void Start()
    {
        Player player = FindObjectOfType<Player>();

        if(player != null)
        {
            isPlayerAlive = true;
            player.OnPlayerDied += Player_OnPlayerDeath;
        }
       
        enemyContainer = transform.Find("EnemyContainer");
        if(enemyContainer != null)
        {
            StartCoroutine(SpawnEnemyRoutine());
        }

        StartCoroutine(SpawnPowerupRoutine());
       
    }



    IEnumerator SpawnEnemyRoutine()
    {
        while(isPlayerAlive)
        {
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-15f, 15f), 10.5f, 0);
            GameObject newEnemy = Instantiate(pfEnemy, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer;

            Enemy enemy = newEnemy.GetComponent<Enemy>();
            OnEnemySpawned?.Invoke(this, new OnEnemySpawnedEventArgs { enemy = enemy });

            yield return new WaitForSeconds(2.5f);
        }

        yield return null;
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
