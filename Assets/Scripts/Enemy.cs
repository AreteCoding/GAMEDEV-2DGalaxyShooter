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

    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
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
            IDamage damageable = other.transform.GetComponent<Player>();

            if (damageable != null)
            {
                damageable.Damage();
            }

            StartCoroutine(DeathRoutine());
        }

        if(other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject);
            StartCoroutine(DeathRoutine());
               
        }
    }

    IEnumerator DeathRoutine()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        moveSpeed = 0;
        firingTimer = Mathf.Infinity;

        OnEnemyDeath?.Invoke(this, EventArgs.Empty);
        audioSource.Play();
        animator.SetTrigger("OnEnemyDeath");
        yield return new WaitForSeconds(2.75f);
        Destroy(this.gameObject);
    }
}

