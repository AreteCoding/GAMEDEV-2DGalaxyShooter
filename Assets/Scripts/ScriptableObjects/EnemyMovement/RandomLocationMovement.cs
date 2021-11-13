using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Random Location", menuName = "Enemy/Movement/Random Location", order = 0)]
public class RandomLocationMovement : EnemyMovement
{
    Vector3 lowerLeft;
    Vector3 upperRight;

    public override void Movement(Enemy enemy)
    {
        lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        upperRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
       // Debug.Log("Lowerleft: " + lowerLeft + " , upperRight: " + upperRight);
        enemy.SetTargetDestination(ChooseNewLocation());
        enemy.StartCoroutine(MovementRoutine(enemy));
    }

    Vector3 ChooseNewLocation()
    {
        return new Vector3( UnityEngine.Random.Range(lowerLeft.x, upperRight.x),
                                         UnityEngine.Random.Range(lowerLeft.y, upperRight.y),
                                         0);
    }

    IEnumerator MovementRoutine(Enemy enemy)
    {
        Vector3 moveDirection = Vector3.Normalize(enemy.TargetDestination - enemy.transform.position);
        while (true)
        {
            enemy.transform.position += moveDirection * enemy.MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(enemy.transform.position, enemy.TargetDestination) < .2)
            {
                yield return new WaitForSeconds(1.5f);
                enemy.SetTargetDestination(ChooseNewLocation());
                moveDirection = Vector3.Normalize(enemy.TargetDestination - enemy.transform.position);
            }

            yield return null;
        }
    }
}
