using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path Movement", menuName = "Enemy/Movement/Path Movement", order = 0)]
public class PathMovement : EnemyMovement
{
    [SerializeField] GameObject pfPath;

    public override void Movement(Enemy enemy)
    {
        enemy.StartCoroutine(MovementRoutine(enemy));
    }

    IEnumerator MovementRoutine(Enemy enemy)
    {
        int nodeIndex = 0;
        List<Transform> pathNodes = GetPathNodes();

        Vector3 moveDirection = Vector3.Normalize(pathNodes[0].position - enemy.transform.position);

        while (true)
        {

            enemy.transform.position += moveDirection * enemy.MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(enemy.transform.position, pathNodes[nodeIndex].position) < .1)
            {
                nodeIndex++;

                if (nodeIndex < pathNodes.Count)
                {                 
                    moveDirection = Vector3.Normalize(pathNodes[nodeIndex].position - enemy.transform.position);
                }
                else
                {
                    break;                              
                }
            }

            yield return null;
        }

        Destroy(enemy.gameObject);
   
    }

    public List<Transform> GetPathNodes()
    {
        List<Transform> pathNodes = new List<Transform>();

        foreach (Transform node in pfPath.transform)
        {
            pathNodes.Add(node);
        }

        return pathNodes;
    }
}
