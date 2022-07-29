using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [Header("Variables")]
    [SerializeField] float movementSpeed = 1f;
    public int hitPoints = 3;

    [Header("Prefabs")]
    [SerializeField] GameObject playerBullet;    
    [SerializeField] ParticleSystem vfx_Explode;
    [SerializeField] AudioClip sfx_laser;
    [SerializeField] AudioClip sfx_explosion;
    [SerializeField] AudioClip sfx_hit;

    AudioSource audioSource;
    UIManager canvas;
    GameManager gameManager;

    [Header("States")]
    public bool bulletFired = false;

    void Start()
    {
        canvas = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && !bulletFired)    //  Only shoots if there is no bullet instantiated to resemble original mechanics
        {
            Shoot();
        }
    }

    void MovePlayer()   //  Gets user input for movement
    {
        float xInput = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;

        transform.Translate(Vector3.right * xInput);
    }

    void Shoot()
    {
        audioSource.PlayOneShot(sfx_laser);
        Vector3 bulletPosition = transform.position + Vector3.up;           // From player position add offset for the bullet position
        Instantiate(playerBullet, bulletPosition, transform.rotation);      // Instantiate bullet

        bulletFired = true;     // The player cannot fire again as long as bulletFired is true
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
        hitPoints--;    // Reduce lives
        audioSource.PlayOneShot(sfx_hit);   //  Play hit sound
        var explosion = Instantiate(vfx_Explode, transform.position, transform.rotation);   // Give user visual cues when hit
        explosion.transform.localScale = new Vector3(2,2,2);    //  Make the explosion bigger
        canvas.livesImages[hitPoints].enabled = false;      //  Disables one life image to resemble amount left
    }

    void GameOver()
    {
        hitPoints = 0;      
        DisableAllLifeImages();
        
        audioSource.PlayOneShot(sfx_explosion);     // Play explosion sound
        var explosion = Instantiate(vfx_Explode, transform.position, transform.rotation);   // Play explosion vfx
        explosion.transform.localScale = new Vector3(2,2,2);    // Make explosion bigger
        gameManager.GameOver();     // Start GameOver sequence
        DestructionSequence();      // Start gameObject destruction sequence
    }

    void DisableAllLifeImages()
    {
        for (int index = 0; index < canvas.livesImages.Length; index++)
        {
            canvas.livesImages[index].enabled = false;
        }
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
}
