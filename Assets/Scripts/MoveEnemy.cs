using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    Vector3 movementDirection = Vector3.right;
    [SerializeField] float lateralSpeed = 1f;
    [SerializeField] float verticalSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemies();
    }
    
    void MoveEnemies()
    {
        transform.Translate(movementDirection * Time.deltaTime * lateralSpeed);    //  Move sideways based on movementDirection
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
