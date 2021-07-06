using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WaveSO", menuName = "Enemy/WaveData/Wave", order = 0)]
public class WaveData : ScriptableObject
{

    [SerializeField] EnemyMovement enemyMovementLogic;
    [SerializeField] GameObject pfEnemy;

    [SerializeField] int enemyTotal;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnFrequency;
    [SerializeField] float timeToNextWave;


    //public List<Transform> GetPathNodes()
    //{
    //    List<Transform> pathNodes = new List<Transform>();

    //    foreach(Transform node in pfPath.transform)
    //    {
    //        pathNodes.Add(node);
    //    }
    //    return pathNodes;
    //}

    public GameObject GetEnemy()
    {
        return pfEnemy;
    }

    public int GetEnemyTotal()
    {
        return enemyTotal;
    }

    public float GetSpawnFrequency()
    {
        return spawnFrequency;
    }

    public float GetTimeToNextWave()
    {
        return timeToNextWave;
    }

    public EnemyMovement GetEnemyMovementLogic()
    {
        return enemyMovementLogic;
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

}
