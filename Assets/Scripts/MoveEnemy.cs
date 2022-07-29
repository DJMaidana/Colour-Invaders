using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [Header("Variables")]
    public float lateralSpeed = 1f;
    [SerializeField] float verticalSpeed = .25f;
    Vector3 movementDirection = Vector3.right;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        lateralSpeed = gameManager.gameDifficulty;
    }

    void Update()
    {
        MoveEnemies();
    }
    
    void MoveEnemies()  //  Move  enemy formation sideways based on movementDirection
    {
        transform.Translate(movementDirection * Time.deltaTime * lateralSpeed);    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SideBounds"))     //  If boundaries are hit
        {
            Vector3 downwards = Vector3.down  * verticalSpeed;
            transform.Translate(downwards);         //  Lowers enemies height

            movementDirection = -movementDirection;     //  Reverses movementDirection

        }
    }
}
