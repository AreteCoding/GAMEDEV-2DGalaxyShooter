using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 9;
    [SerializeField] float timeToLive = 1;
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

   
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
