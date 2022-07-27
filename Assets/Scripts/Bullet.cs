using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1;
    
    PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();      //  Get PlayerController reference
    }

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        Vector2 upDirection = Vector2.up * Time.deltaTime * bulletSpeed;
        transform.Translate(upDirection);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        player.bulletFired = false;   // Enable the player to shoot again
        Destroy(gameObject);
    }
}
