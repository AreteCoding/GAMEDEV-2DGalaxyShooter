using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boundry Bounce", menuName = "Enemy/Movement/Boundry Bounce", order = 0)]
public class ZigZagMovement : EnemyMovement
{
    public override void Movement(Enemy enemy)
    {
        enemy.StartCoroutine(MovementRoutine(enemy));
    }

    IEnumerator MovementRoutine(Enemy enemy)
    {
        Vector3 moveDirection;
        float xSpawnPosition;
        Vector3 xDirection = Vector3.left;

        while (true)
        {
            moveDirection = Vector3.down + xDirection;
            enemy.transform.position += moveDirection * enemy.MoveSpeed * Time.deltaTime;

            if (enemy.transform.position.y < enemy.YMovementThreshhold)
            {
                xSpawnPosition = UnityEngine.Random.Range(-12f, 12f);
                enemy.transform.position = new Vector3(xSpawnPosition, enemy.YRespawnHeight, 0);
            }

            if(enemy.transform.position.x > 12 || enemy.transform.position.x < -12)
            {
                xDirection = -xDirection;
            }

            yield return null;
        }
    }
}
