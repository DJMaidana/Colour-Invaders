using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] int hitPoints = 5;
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
        DestroySequence();
    }

    void DestroySequence()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        Invoke("DestroyGameObject", 1f);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
