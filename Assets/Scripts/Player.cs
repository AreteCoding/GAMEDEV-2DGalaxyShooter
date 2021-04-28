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
    public void SetVelocity(Vector3 moveVector)
    {
        this.moveVector = moveVector;
    }

    void Update()
    {
        MovePlayer();
        FireWeapon();
    }

    private void MovePlayer()
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

    private void FireWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(pfProjectile, firePosition.position, Quaternion.identity);
        }
    }
}
