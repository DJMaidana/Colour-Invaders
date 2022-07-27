using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1;

    void Update()
    {
        Vector2 upDirection = Vector2.up * Time.deltaTime * bulletSpeed;
        transform.Translate(upDirection);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("AAAAAA");
        Destroy(gameObject);
    }
}
