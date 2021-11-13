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
    public float MoveSpeed => moveSpeed;

    [SerializeField] GameObject pfProjectile;

    [SerializeField] float fireRate;
    [SerializeField] float fireRateVariation;
    float firingTimer;

    [SerializeField] float yMovementThreshhold;
    [SerializeField] float yRespawnHeight;
    public float YMovementThreshhold => yMovementThreshhold;
    public float YRespawnHeight => yRespawnHeight;

    Animator animator;
    AudioSource audioSource;
    Vector3 targetDestination;
    public Vector3 TargetDestination => targetDestination;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
  
    }
    void Update()
    {
        ShootingLogic();
      //  MovementLogic();
    }

    private void ShootingLogic()
    {
        firingTimer -= Time.deltaTime;

        if(firingTimer <= 0)
        {
            GameObject newProjectile = Instantiate(pfProjectile, transform.position, Quaternion.identity);
           // newProjectile.GetComponent<EnemyProjectile>().Setup(Vector3.down);
            firingTimer = fireRate + UnityEngine.Random.Range(-fireRateVariation, fireRateVariation);
        }
    }

    void MovementLogic()
    {
        //if(moveDirection != Vector3.zero)
        //{
        //    transform.position += moveDirection * enemy.MoveSpeed * Time.deltaTime;
        //}
    }

    public void SetTargetDestination(Vector3 destination)
    {
        targetDestination = destination;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
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
        gameObject.GetComponent<Collider2D>().enabled = false;
        moveSpeed = 0;
        firingTimer = Mathf.Infinity;

        OnEnemyDeath?.Invoke(this, EventArgs.Empty);
        audioSource.Play();

        if(animator != null)
        {
            animator.SetTrigger("OnEnemyDeath");
        }
       
        yield return new WaitForSeconds(2.75f);
        Destroy(this.gameObject);
    }
}

