using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hitPoints = 1;
    [SerializeField] int minShootingPeriod = 5;
    [SerializeField] int maxShootingPeriod = 10;
        

    [SerializeField] GameObject enemyBullet;
    [SerializeField] ParticleSystem vfx_Explode;
    [SerializeField] AudioClip sfx_laser;
    [SerializeField] AudioClip sfx_explosion;

    AudioSource audioSource;
    GameManager gameManager;


    public string enemyType = "Set to respective color";
    public bool alreadyChecked = false;
    public bool isInFront;
    bool hasFired;
    float firingRate;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()   //  Checks if the enemy is the first of its column to allow it to shoot
    {
        if (isInFront)
        {
            if (!hasFired)
            {
                firingRate = Random.Range(minShootingPeriod, maxShootingPeriod);  // Shoots with varying frequency
                Invoke("Shoot", firingRate);
                hasFired = true;                //  locks firing ability until the method Shoot() reloads it
            }

        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(sfx_laser);
        Vector3 bulletPosition = transform.position + Vector3.down;           // From enemy position get offset for the bullet position
        Quaternion bulletRotation = Quaternion.Euler(0f, 0f, -180f);          // Rotate Bullet so it goes down
        Instantiate(enemyBullet, bulletPosition, bulletRotation);       
        hasFired = false;                                                     // Enables firing
    }

    void OnCollisionEnter2D(Collision2D other)      //  Checks if the enemy was hit by a bullet or crashed against Covers
    {
        switch (other.gameObject.tag)   
        {
            case "Player_Bullet":       //  Sequence for being hit by a Player Bullet

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
            gameManager.ReduceRemainingEnemies(1);  
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        audioSource.PlayOneShot(sfx_explosion);
        Instantiate(vfx_Explode, transform.position, transform.rotation);
        gameManager.IncreaseDifficulty();
        DestructionSequence();
    }

    void DestructionSequence()  //  This sequence eliminates all intervention of the GameObject in the world, but waits for sound
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        Invoke("DestroyGameObject", 1f);    //  Destroys after a second to allow sounds to keep playing.
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    void AddSelfToList()    // Adds itself to the Game Manager list containing the enemies hit by Player Bullets
    {
        alreadyChecked = true;      //  Flags itself so the neighboring enemies don't count it again
        gameManager.listEnemiesConnected.Add(gameObject.GetComponent<Enemy>());
    }

    public void AddNeighborsToList()
    {
        gameManager.GetConnectedEnemies();      
    }

    public void CheckNeighbors()    // Casts a ray in four directions and decides if the adjacent enemies are of the same color
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
