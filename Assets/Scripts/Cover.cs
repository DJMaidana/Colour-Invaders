using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] int hitPoints = 5;

    [Header("Prefabs")]
    [SerializeField] ParticleSystem vfx_Explode;
    [SerializeField] AudioClip sfx_explosion;
    [SerializeField] AudioClip sfx_hit;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (hitPoints > 1)
        {
            DamageCover();
        }
        else
        {
            DestroyCover();
        }
    }

    void DamageCover()
    {
        audioSource.PlayOneShot(sfx_hit);
        hitPoints--;
    }

    void DestroyCover()
    {
        audioSource.PlayOneShot(sfx_explosion);
        Instantiate(vfx_Explode, transform.position, transform.rotation);
        DestructionSequence();
    }

    void DestructionSequence()      //  This sequence eliminates all intervention of the GameObject in the world, but waits for sound
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
