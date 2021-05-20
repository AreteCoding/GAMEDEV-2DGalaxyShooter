using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public event EventHandler OnScoreUpdated;

    int playerScore;
    public int PlayerScore => playerScore;

    SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        spawnManager.OnEnemySpawned += SpawnManager_OnEnemySpawned;

        playerScore = 0;
    }

    void SpawnManager_OnEnemySpawned(object sender, OnEnemySpawnedEventArgs e)
    {
        e.enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
    }

    void Enemy_OnEnemyDeath(object sender, EventArgs e)
    {
        Enemy enemy = sender as Enemy;
        playerScore += enemy.ScorePoints;
        OnScoreUpdated?.Invoke(this, EventArgs.Empty);
    }
}
