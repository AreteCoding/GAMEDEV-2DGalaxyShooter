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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamage damageable = other.transform.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.Damage();
            }
          
            Destroy(this.gameObject);
        }
    }
}
