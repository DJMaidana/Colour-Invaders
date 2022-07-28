using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hitPoints = 1;

    [SerializeField] GameObject enemyBullet;

    public bool isInFront;
    bool hasFired;
    float firingRate;

    void Update()
    {
        if (isInFront)
        {
            if (!hasFired)
            {
                firingRate = Random.Range(5,10);
                Invoke("Shoot", firingRate);
                hasFired = true;                //  locks firing ability until the method Shoot() reloads it
            }

        }
    }

    void Shoot()
    {
        Vector3 bulletPosition = transform.position + Vector3.down;           // From enemy position get offset for the bullet position
        Quaternion bulletRotation = Quaternion.Euler(0f, 0f, -180f);          // Rotate Bullet so it goes down
        Instantiate(enemyBullet, bulletPosition, bulletRotation);       
        hasFired = false;                                                     // Enables firing
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)   
        {
            case "Player_Bullet":       //  Sequence for being shot by the player

                HitByPlayer();
                break;

            case "Cover":               //  Sequence for crashing against Covers

                HitSelf();
                break;
        }
    }

    void HitByPlayer()
    {   
        if (hitPoints > 1)
        {
            //Add score on hit
            hitPoints--;
        }
        else
        {
            //Calculate neighbors
            //Calculate score on death
            EnemyDeath();
        }
    }

    void HitSelf()
    {
        if (hitPoints > 1)
        {
            hitPoints--;
        }
        else
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
