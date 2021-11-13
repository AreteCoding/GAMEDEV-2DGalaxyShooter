using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHoming : MonoBehaviour
{
    [SerializeField] float moveSpeed = 9;
    [SerializeField] float timeToLive = 10;

    
    Transform target;
    Vector3 moveDirection;
    float homingCounter;

    void Start()
    {
        Player player = FindObjectOfType<Player>();
        if(player != null)
        {
            target = player.gameObject.transform;
          
        }
        else
        {
            moveDirection = Vector3.down;
        }

        homingCounter = timeToLive / 2;
        Destroy(gameObject, timeToLive);

    }

   
    void Update()
    {
        homingCounter -= Time.deltaTime;

        if(homingCounter > 0 && target != null)
        {
            if(target != null)
            {
                moveDirection = (target.position - transform.position).normalized;
            }
            
        }     
       
        transform.position +=  moveDirection * moveSpeed * Time.deltaTime;
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
