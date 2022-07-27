using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hitPoints = 1;

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
