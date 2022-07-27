using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    [SerializeField] GameObject playerBullet;

    public bool bulletFired = false;

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && !bulletFired)    //  Only shoots if there is no bullet instantiated to resemble original mechanics
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

        bulletFired = true;
    }
}
