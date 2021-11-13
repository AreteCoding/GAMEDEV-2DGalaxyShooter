using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 9;
    [SerializeField] float timeToLive = 3;

    Vector3 moveDirection;
    void Start()
    {
        moveDirection = Vector3.down;
        Destroy(gameObject, timeToLive);
    }

   
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void Setup(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
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
