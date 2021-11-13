using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAggressive : MonoBehaviour
{
    public event EventHandler OnEnemyDeath;

    [SerializeField] int scorePoints = 10;
    public int ScorePoints => scorePoints;

    Vector3 lowerLeft;
    Vector3 upperRight;

    [SerializeField] float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;
    [SerializeField] float ramSpeed = 10f;

    [SerializeField] GameObject pfProjectile;

    [SerializeField] float fireRate;
    [SerializeField] float fireRateVariation;
    float firingTimer;

    Animator animator;
    AudioSource audioSource;
    Vector3 targetDestination;
    public Vector3 TargetDestination => targetDestination;

    Coroutine movementRoutine = null;
    bool activeMovementRoutine = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        upperRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    private void Start()
    {
        StartMovementRoutine();
    }
    void Update()
    {
  
    }

    private void ShootingLogic()
    {
        firingTimer -= Time.deltaTime;

        if (firingTimer <= 0)
        {
            GameObject newProjectile = Instantiate(pfProjectile, transform.position, Quaternion.identity);
            // newProjectile.GetComponent<EnemyProjectile>().Setup(Vector3.down);
            firingTimer = fireRate + UnityEngine.Random.Range(-fireRateVariation, fireRateVariation);
        }
    }

    Vector3 ChooseNewLocation()
    {
        return new Vector3(UnityEngine.Random.Range(lowerLeft.x, upperRight.x),
                                         UnityEngine.Random.Range(lowerLeft.y, upperRight.y),
                                         0);
    }

    public void StartRammingCoroutine(GameObject goTarget)
    {
        if(activeMovementRoutine) { return; }

        if(movementRoutine != null)
        {
            StopCoroutine(movementRoutine);
        }

        activeMovementRoutine = true;
        movementRoutine = StartCoroutine(RammingCoroutine(goTarget));
    }

    public void StartMovementRoutine()
    {
        if (movementRoutine != null)
        {
            StopCoroutine(movementRoutine);
        }

        activeMovementRoutine = false;
        movementRoutine = StartCoroutine(MovementRoutine());
    }

    IEnumerator RammingCoroutine(GameObject target)
    {
        Vector3 rammingLocation = target.transform.position;
        // start ramming animation
        yield return new WaitForSeconds(.8f);    

        bool isRamming = true;
        while (isRamming)
        {
            float step = ramSpeed * Time.deltaTime; // calculate max move distance this frame
            transform.position = Vector3.MoveTowards(transform.position, rammingLocation, step);

            if (Vector3.Distance(transform.position, rammingLocation) < .2f)
            {
                yield return new WaitForSeconds(.5f);
                isRamming = false;
             
            }

            yield return null;
        }

        StartMovementRoutine();
    }

    IEnumerator MovementRoutine()
    {
        targetDestination = ChooseNewLocation();

        Vector3 moveDirection = Vector3.Normalize(targetDestination - transform.position);

        while (true)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetDestination) < 1)
            {
                yield return new WaitForSeconds(1.5f);
              
                targetDestination = ChooseNewLocation();
                moveDirection = Vector3.Normalize(targetDestination - transform.position);
            }

            yield return null;
        }
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
        if (other.CompareTag("Player"))
        {
            IDamage damageable = other.transform.GetComponent<Player>();

            if (damageable != null)
            {
                damageable.Damage();
            }

            StartCoroutine(DeathRoutine());
        }

        if (other.CompareTag("PlayerProjectile"))
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

        if (animator != null)
        {
            animator.SetTrigger("OnEnemyDeath");
        }

        yield return new WaitForSeconds(2.75f);
        Destroy(this.gameObject);
    }
}

