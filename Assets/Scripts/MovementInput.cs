using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    IVelocity iVelocity;

    Vector3 moveDirection;
    //float horizontalInput;
    //float verticalInput;

    private void Awake()
    {
        iVelocity = GetComponent<IVelocity>();
    }
    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        //iVelocity.SetVelocity(new Vector3(horizontalInput, verticalInput, 0));
        iVelocity.SetVelocity(moveDirection);
        
    }
}
