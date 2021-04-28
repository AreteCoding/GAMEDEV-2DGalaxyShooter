using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    float timeToLive = 1f;

    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;  
    
    }
}
