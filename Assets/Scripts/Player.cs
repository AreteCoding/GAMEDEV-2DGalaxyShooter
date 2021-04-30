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
    public void SetVelocity(Vector3 moveVector)
    {
        this.moveVector = moveVector;
    }

    void Update()
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

    public void Damage()
    {
        Debug.Log("Version control experiment test for Damage function");
    }
}
