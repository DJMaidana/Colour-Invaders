using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    [SerializeField] GameObject playerBullet;

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void MovePlayer()
    {
        float xInput = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;

        transform.Translate(Vector3.right * xInput);
    }

    void Shoot()
    {
        Vector3 bulletPosition = transform.position + Vector3.up;           // From player position get offset for the bullet position
        Instantiate(playerBullet, bulletPosition, transform.rotation);      // Instantiate bullet
    }
}
