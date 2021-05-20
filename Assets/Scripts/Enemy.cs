using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    public event EventHandler OnEnemyDeath;

    [SerializeField] int scorePoints = 5;
    public int ScorePoints => scorePoints;

    [SerializeField] float moveSpeed = 5f;

    [SerializeField] GameObject pfProjectile;

    [SerializeField] float fireRate;
    [SerializeField] float fireRateVariation;
    float firingTimer;
    
    [SerializeField] float yMovementThreshhold;
    [SerializeField] float yRespawnHeight;
    void Update()
    {
        MovementLogic();
        FiringLogic();
    }

    private void MovementLogic()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if(transform.position.y < yMovementThreshhold)
        {
            float xSpawnPosition = UnityEngine.Random.Range(-3f, 3f);

            transform.position = new Vector3(xSpawnPosition, yRespawnHeight, 0);
        }
    }

    private void FiringLogic()
    {
        firingTimer -= Time.deltaTime;

        if(firingTimer <= 0)
        {
            Instantiate(pfProjectile, transform.position, Quaternion.identity);
            firingTimer = fireRate + UnityEngine.Random.Range(-fireRateVariation, fireRateVariation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player)
            {
                player.Damage();
            }

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if(other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject);
            DeathLogic();        
        }
    }

    void DeathLogic()
    {
        OnEnemyDeath?.Invoke(this, EventArgs.Empty);

        Destroy(this.gameObject);
    }
}

