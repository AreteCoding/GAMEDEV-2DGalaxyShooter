using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IVelocity, IDamage
{
    public event EventHandler OnPlayerDamaged;
    public event EventHandler OnPlayerHealed;
    public event EventHandler OnPlayerDied;
    public event EventHandler OnPlayerFired;
    public event EventHandler OnPlayerReloaded;
    public event EventHandler OnThrustersUsed;

    #region Movement Variables

    [SerializeField] float baseMoveSpeed;   
    float moveSpeedMultiplier = 0;  // tracks speed from powerups
    float currentMoveSpeed;

    [SerializeField] float thrustMultiplier = .2f;
    [SerializeField] float thrustAmountMax = 7f;
    float currentThrustAmount;
    float thrustSpeed;

    Vector3 moveVector; //obtained from the IVelocity component

    [SerializeField] float upperBound;
    [SerializeField] float lowerBound;
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;
    Vector3 boundedPosition;
    #endregion

    #region Firing/Ammo Variables

    [SerializeField] Transform firePosition;
    [SerializeField] GameObject pfStartingProjectile;
    public GameObject CurrentProjectile => currentProjectile;
    GameObject currentProjectile;

    [SerializeField] float fireRate;
    float cooldownTimer;
    [SerializeField] float reloadRate;  // should be slower than fireRate
    float reloadTimer;

    public int ProjectileCapacity => projectileCapacity;
    [SerializeField] int projectileCapacity;

    public int ProjectileCount => projectileCount;
    int projectileCount;
    #endregion

    [SerializeField] GameObject leftEngineDamaged;
    [SerializeField] GameObject rightEngineDamaged;

    public int PlayerLives => playerLives;
    [SerializeField] int playerLives = 3;

    Animator animator;
    private void Awake()
    {

        animator = GetComponent<Animator>();

        leftEngineDamaged.SetActive(false);
        rightEngineDamaged.SetActive(false);

        projectileCount = projectileCapacity;
        currentThrustAmount = thrustAmountMax;
        thrustSpeed = baseMoveSpeed + (baseMoveSpeed * thrustMultiplier);
    }
    private void Start()
    {  
        SetProjectile(pfStartingProjectile);
    }
  
    void Update()
    {
        MovementLogic();
        // ReloadLogic();
        CooldownLogic();    
        FiringLogic();     
    }
    public void SetVelocity(Vector3 moveVector)
    {
        this.moveVector = moveVector;
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

    public void AddAmmo(int amount)
    {
        projectileCount = Mathf.Clamp(projectileCount += amount, 0, projectileCapacity);
        OnPlayerReloaded?.Invoke(this, EventArgs.Empty);
    }

    public void AddHealth(int amount)
    {
        playerLives += amount;
        OnPlayerHealed?.Invoke(this, EventArgs.Empty);
    }

    public float GetThrusterAmount()
    {
        return currentThrustAmount/thrustAmountMax;
    }

    private void MovementLogic()
    {

        transform.position += moveVector * currentMoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift) && thrustAmountMax > 0)
        {
            currentMoveSpeed = thrustSpeed + moveSpeedMultiplier;
            Mathf.Clamp(currentThrustAmount -= Time.deltaTime, 0, thrustAmountMax);
            OnThrustersUsed?.Invoke(this, EventArgs.Empty);
            
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
            
            projectileCount--;
            OnPlayerFired?.Invoke(this, EventArgs.Empty);
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
            OnPlayerDied?.Invoke(this, EventArgs.Empty);
            Destroy(this.gameObject);           
        }
    }
}
