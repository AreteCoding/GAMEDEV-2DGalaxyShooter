using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupVisual : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float timeToLive = 2f;
    [SerializeField] GameObject pfPowerup;
    [SerializeField] AudioClip audioClip;

    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
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
