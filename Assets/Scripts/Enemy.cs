using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hitPoints = 1;

    [SerializeField] GameObject enemyBullet;
    [SerializeField] ParticleSystem vfx_Explode;

    GameManager gameManager;

    public string enemyType = "Set to respective color";
    public bool alreadyChecked = false;
    public bool isInFront;
    bool hasFired;
    float firingRate;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

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

    public void HitByPlayer()
    {   
        if (hitPoints > 1)
        {
            hitPoints--;
        }
        else
        {
            AddSelfToList();          
            AddNeighborsToList();
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

    public void EnemyDeath()
    {
        Instantiate(vfx_Explode, transform.position, transform.rotation);
        gameManager.IncreaseDifficulty();
        Destroy(gameObject);
    }

    void AddSelfToList()
    {
        alreadyChecked = true;
        gameManager.listEnemiesConnected.Add(gameObject.GetComponent<Enemy>());
    }

    public void AddNeighborsToList()
    {
        gameManager.GetConnectedEnemies();
    }

    public void CheckNeighbors()
    {
        CheckUp();
        CheckDown();
        CheckLeft();
        CheckRight();
    }

    void CheckUp()
    {
        float yRayDistance = 1f;    // The distance between adjacent enemies, otherwise might flag enemies that are far away.

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, yRayDistance);   //  Cast a ray upwards, returns the first object it collides with
        
        if (hit.collider != null)   // If there is a collision
        {
            bool hitIsEnemy = hit.collider.CompareTag("Enemy");

            if (hitIsEnemy)    // If it's an enemy
            {
                bool hitAlreadyChecked = hit.collider.GetComponent<Enemy>().alreadyChecked;
                string enemyType = hit.collider.GetComponent<Enemy>().enemyType;

                if (!hitAlreadyChecked && enemyType == this.enemyType)     // If is the same color and it hasn't been added to the List of neighboring enemies
                {
                    gameManager.listEnemiesConnected.Add(hit.collider.gameObject.GetComponent<Enemy>());    // Add it to the list

                    hit.collider.GetComponent<Enemy>().alreadyChecked = true;                                                             // Check it as added so the recursions don't add it again
                    hit.collider.GetComponent<Enemy>().CheckNeighbors();                                    // Checks the collision neighbors recursively
                }
            }
        }
    }

    void CheckDown()
    {
        float yRayDistance = 1f;    // The distance between adjacent enemies, otherwise might flag enemies that are far away.

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, yRayDistance);   //  Cast a ray downwards, returns the first object it collides with
        
        if (hit.collider != null)   // If there is a collision
        {
            bool hitIsEnemy = hit.collider.CompareTag("Enemy");

            if (hitIsEnemy)    // If it's an enemy
            {
                bool hitAlreadyChecked = hit.collider.GetComponent<Enemy>().alreadyChecked;
                string enemyType = hit.collider.GetComponent<Enemy>().enemyType;

                if (!hitAlreadyChecked && enemyType == this.enemyType)     // If is the same color and it hasn't been added to the List of neighboring enemies
                {
                    gameManager.listEnemiesConnected.Add(hit.collider.gameObject.GetComponent<Enemy>());    // Add it to the list

                    hit.collider.GetComponent<Enemy>().alreadyChecked = true;                                                             // Check it as added so the recursions don't add it again
                    hit.collider.GetComponent<Enemy>().CheckNeighbors();                                    // Checks the collision neighbors recursively
                }
            }
        }
    }

    void CheckLeft()
    {
        float xRayDistance = 2f;    // The distance between adjacent enemies, otherwise might flag enemies that are far away.

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, xRayDistance);   //  Cast a ray to the left, returns the first object it collides with
        
        if (hit.collider != null)   // If there is a collision
        {
            bool hitIsEnemy = hit.collider.CompareTag("Enemy");

            if (hitIsEnemy)    // If it's an enemy
            {
                bool hitAlreadyChecked = hit.collider.GetComponent<Enemy>().alreadyChecked;
                string enemyType = hit.collider.GetComponent<Enemy>().enemyType;

                if (!hitAlreadyChecked && enemyType == this.enemyType)     // If is the same color and it hasn't been added to the List of neighboring enemies
                {
                    gameManager.listEnemiesConnected.Add(hit.collider.gameObject.GetComponent<Enemy>());    // Add it to the list

                    hit.collider.GetComponent<Enemy>().alreadyChecked = true;                                                             // Check it as added so the recursions don't add it again
                    hit.collider.GetComponent<Enemy>().CheckNeighbors();                                    // Checks the collision neighbors recursively
                }
            }
        }
    }

    void CheckRight()
    {
        float xRayDistance = 2f;    // The distance between adjacent enemies, otherwise might flag enemies that are far away.

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, xRayDistance);   //  Cast a ray to the right, returns the first object it collides with
        
        if (hit.collider != null)   // If there is a collision
        {
            bool hitIsEnemy = hit.collider.CompareTag("Enemy");

            if (hitIsEnemy)    // If it's an enemy
            {
                bool hitAlreadyChecked = hit.collider.GetComponent<Enemy>().alreadyChecked;
                string enemyType = hit.collider.GetComponent<Enemy>().enemyType;

                if (!hitAlreadyChecked && enemyType == this.enemyType)     // If is the same color and it hasn't been added to the List of neighboring enemies
                {
                    gameManager.listEnemiesConnected.Add(hit.collider.gameObject.GetComponent<Enemy>());    // Add it to the list

                    hit.collider.GetComponent<Enemy>().alreadyChecked = true;                                                             // Check it as added so the recursions don't add it again
                    hit.collider.GetComponent<Enemy>().CheckNeighbors();                                    // Checks the collision neighbors recursively
                }
            }
        }
    }

}
