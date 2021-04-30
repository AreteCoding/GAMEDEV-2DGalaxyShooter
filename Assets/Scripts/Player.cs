using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IVelocity
{
    [SerializeField] float moveSpeed;
    Vector3 moveVector;

    [SerializeField] float upperBound;
    [SerializeField] float lowerBound;
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;
    Vector3 boundedPosition;

    [SerializeField] Transform firePosition;
    [SerializeField] GameObject pfProjectile;

    //Cooldown System
    [SerializeField] float fireRate;
    float cooldownTimer;
    [SerializeField] float reloadRate;  // should be slower than fireRate
    float reloadTimer;
    [SerializeField] int projectileCapacity;
    int projectileCount;

    [SerializeField] int playerLives = 3;
    private void Start()
    {
        projectileCount = projectileCapacity;
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

    private void FiringLogic()
    {

        if(Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0 && projectileCount > 0)
        {
            Instantiate(pfProjectile, firePosition.position, Quaternion.identity);

            projectileCount--;
            cooldownTimer = fireRate;
            reloadTimer = reloadRate;
        }
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

    public void Damage()
    {
        playerLives--;

        if(playerLives < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
