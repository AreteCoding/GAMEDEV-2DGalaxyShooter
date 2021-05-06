using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject pfPowerupVisual;
    [SerializeField] GameObject pfEnemy;
    Transform enemyContainer;

    bool isPlayerAlive;

    void Start()
    {
        Player player = FindObjectOfType<Player>();

        if(player != null)
        {
            isPlayerAlive = true;
            player.OnPlayerDeath += Player_OnPlayerDeath;
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
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-10.5f, 10.5f), 6, 0);
            GameObject newEnemy = Instantiate(pfEnemy, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer;
            yield return new WaitForSeconds(2.5f);
        }

        yield return null;
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (isPlayerAlive)
        {
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-10.5f, 10.5f), 6, 0);
            GameObject newPowerup = Instantiate(pfEnemy, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(UnityEngine.Random.Range(8, 15));
        }

        yield return null;
    }

    void Player_OnPlayerDeath(object sender, EventArgs e)
    {
        isPlayerAlive = false;
    }
}
