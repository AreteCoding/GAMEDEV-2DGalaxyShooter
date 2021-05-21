using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 19f;
    [SerializeField] GameObject explosionVFX;
    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            GameObject newAst = Instantiate(explosionVFX, transform.position, Quaternion.identity);
     
            Destroy(collision.gameObject);
            Destroy(gameObject, .25f);
        }
    }
}
