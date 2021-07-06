using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple Down", menuName = "Enemy/Movement/Simple Down", order = 0)]
public class SimpleDownMovement : EnemyMovement
{
    public override void Movement(Enemy enemy)
    {
        enemy.StartCoroutine(MovementRoutine(enemy));
    }

    IEnumerator MovementRoutine(Enemy enemy)
    {
        Vector3 moveDirection = Vector3.down;
        float xSpawnPosition;
      
        while (true)
        {
            
            enemy.transform.position += moveDirection * enemy.MoveSpeed * Time.deltaTime;

            if (enemy.transform.position.y < enemy.YMovementThreshhold)
            {
                xSpawnPosition = UnityEngine.Random.Range(-12f, 12f);
                enemy.transform.position = new Vector3(xSpawnPosition, enemy.YRespawnHeight, 0);
            }

            yield return null;
        }
    }
}
