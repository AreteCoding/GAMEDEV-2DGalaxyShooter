using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IVelocity
{
    public event EventHandler OnPlayerDeath;

    [SerializeField] float moveSpeed;
    public float MoveSpeed => moveSpeed;
    Vector3 moveVector;

    [SerializeField] float upperBound;
    [SerializeField] float lowerBound;
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;
    Vector3 boundedPosition;

    [SerializeField] Transform firePosition;
    [SerializeField] GameObject pfStartingProjectile;
    [SerializeField] GameObject leftEngineDamaged;
    [SerializeField] GameObject rightEngineDamaged;
    GameObject shield;

    Animator animator;

    public GameObject CurrentProjectile => currentProjectile;
    GameObject currentProjectile;

    //Cooldown System
    [SerializeField] float fireRate;
    float cooldownTimer;
    [SerializeField] float reloadRate;  // should be slower than fireRate
    float reloadTimer;
    [SerializeField] int projectileCapacity;
    int projectileCount;

    [SerializeField] int playerLives = 3;
    public int PlayerLives => playerLives;

    private void Awake()
    {
        shield = transform.Find("shield").gameObject;
        animator = GetComponent<Animator>();

        leftEngineDamaged.SetActive(false);
        rightEngineDamaged.SetActive(false);
    }
    private void Start()
    {
        projectileCount = projectileCapacity;
        SetProjectile(pfStartingProjectile);

    }
    public void SetVelocity(Vector3 moveVector)
    {
        this.moveVector = moveVector;
    }

    void Update()
    {
        MovementLogic();
        ReloadLogic();
        CooldownLogic();    
        FiringLogic();
       
    }

    public void SetProjectile(GameObject projectile)
    {
        currentProjectile = projectile;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void SetShield(GameObject shield)
    {
        this.shield = shield;
    }

    void ActivateShield()
    {
        shield.SetActive(true);
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

    public void AddMoveSpeed(float speed)
    {
        this.moveSpeed += speed;
    }


    private void MovementLogic()
    {
        transform.position += moveVector * moveSpeed * Time.deltaTime;
        CheckBounds();
    }

    private void CheckBounds()
    {
        boundedPosition.x = Mathf.Clamp(transform.position.x, leftBound, rightBound);
        boundedPosition.y = Mathf.Clamp(transform.position.y, lowerBound, upperBound);
        transform.position = boundedPosition;
    }

    private void CooldownLogic()
    {
        cooldownTimer -= Time.deltaTime;
    }

    private void ReloadLogic()
    {
        reloadTimer -= Time.deltaTime;

        if(projectileCount < projectileCapacity && reloadTimer <=0)
        {
            projectileCount++;
            reloadTimer = reloadRate;
        }
    }

    private void FiringLogic()
    {

        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0 && projectileCount > 0)
        {
            Instantiate(currentProjectile, firePosition.position, Quaternion.identity);

            projectileCount--;
            cooldownTimer = fireRate;
        }
    }

    public void Damage()
    {
        playerLives--;
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);

        if(playerLives == 2)
        {
            leftEngineDamaged.SetActive(true);
        }

        if (playerLives == 1)
        {
            rightEngineDamaged.SetActive(true);
        }

        if (playerLives < 1)
        {
          //  animator.SetTrigger("OnPlayerDeath");
            Destroy(this.gameObject);           
        }
    }
}
