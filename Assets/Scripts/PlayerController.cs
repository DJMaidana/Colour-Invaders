using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] int hitPoints = 3;

    [SerializeField] GameObject playerBullet;    

    [SerializeField] ParticleSystem vfx_Explode;

    UIManager canvas;

    public bool bulletFired = false;

    void Start()
    {
        canvas = FindObjectOfType<UIManager>();
    }

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

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)   
        {
            case "Enemy_Bullet":       //  Sequence for being shot by an enemy

                HitByEnemy();
                break;

            case "Enemy":               //  Sequence for crashing against enemies

                GameOver();
                break;
        }
    }

    void HitByEnemy()
    {
        if (hitPoints > 1)
        {
            PlayerDamaged();
        }
        else
        {
            GameOver();
        }
    }

    void PlayerDamaged()
    {
        hitPoints--;
        canvas.livesImages[hitPoints].enabled = false;
    }

    void GameOver()
    {
        hitPoints = 0;
        canvas.livesImages[hitPoints].enabled = false;

        Instantiate(vfx_Explode, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
