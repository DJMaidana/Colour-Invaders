using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] int hitPoints = 5;
    [SerializeField] ParticleSystem vfx_Explode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        hitPoints--;
    }

    void DestroyCover()
    {
        Instantiate(vfx_Explode, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
