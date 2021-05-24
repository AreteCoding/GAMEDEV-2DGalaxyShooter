using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IVelocity, IDamage
{
    public event EventHandler OnPlayerDamaged;
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnPlayerFired;

    [SerializeField] float baseMoveSpeed;
    [SerializeField] float thrustMultiplier = .2f;
    float thrustSpeed;
    [SerializeField] float thrustAmountMax = 7f;
    float currentThrustAmount;
    float moveSpeedMultiplier = 0;  // tracks speed from powerups
    float currentMoveSpeed;

    Vector3 moveVector; //obtained from the IVelocity component

    [SerializeField] float upperBound;
    [SerializeField] float lowerBound;
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;
    Vector3 boundedPosition;

    [SerializeField] Transform firePosition;
    [SerializeField] GameObject pfStartingProjectile;
    [SerializeField] GameObject leftEngineDamaged;
    [SerializeField] GameObject rightEngineDamaged;

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

        animator = GetComponent<Animator>();

        leftEngineDamaged.SetActive(false);
        rightEngineDamaged.SetActive(false);

        currentThrustAmount = thrustAmountMax;
        thrustSpeed = baseMoveSpeed + (baseMoveSpeed * thrustMultiplier);
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
        currentMoveSpeed = moveSpeed;
    }

    public void AddMoveSpeedMultiplier(float speed)
    {
        moveSpeedMultiplier += speed;
    }


    private void MovementLogic()
    {

        transform.position += moveVector * currentMoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift) && thrustAmountMax > 0)
        {
            currentMoveSpeed = thrustSpeed + moveSpeedMultiplier;
            Mathf.Clamp(currentThrustAmount -= Time.deltaTime, 0, thrustAmountMax);
            
        }
        else 
        {
            currentMoveSpeed = baseMoveSpeed + moveSpeedMultiplier;        
        }

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
            OnPlayerFired?.Invoke(this, EventArgs.Empty);

            projectileCount--;
            cooldownTimer = fireRate;
        }
    }

    public void Damage()
    {
        playerLives--;
        OnPlayerDamaged?.Invoke(this, EventArgs.Empty);

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
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            Destroy(this.gameObject);           
        }
    }
}
