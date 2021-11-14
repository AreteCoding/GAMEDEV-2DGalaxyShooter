using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupVisual : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float timeToLive = 2f;
    [SerializeField] GameObject pfPowerup;
    [SerializeField] AudioClip audioClip;

    Transform target;
    Vector3 moveDirection;

    private void Awake()
    {
        moveDirection = Vector3.down;
    }

    private void Start()
    {
        target = null;
        Destroy(gameObject, timeToLive);
    }
    void Update()
    {
       
        if(target != null)
        {
            moveDirection = Vector3.Normalize(target.position - transform.position);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        moveSpeed *= 1.5f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            if(player != null)
            {
                GameObject newPowerup = Instantiate(pfPowerup);
                //  newPowerup.GetComponent<PowerupTripleShot>().SetPlayer(player);     
                newPowerup.GetComponent<IPowerup>().SetPlayer(player);

                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }

            Destroy(gameObject);
        }
    }
}
