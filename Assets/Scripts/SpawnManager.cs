using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{

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
            StartCoroutine(SpawnRoutine());
        }
       
    }



    IEnumerator SpawnRoutine()
    {
        while(isPlayerAlive)
        {
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-4.2f, 4.2f), 6, 0);
            GameObject newEnemy = Instantiate(pfEnemy, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer;
            yield return new WaitForSeconds(2.5f);
        }

        yield return null;
    }

    void Player_OnPlayerDeath(object sender, EventArgs e)
    {
        isPlayerAlive = false;
    }
}
