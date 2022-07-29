using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("How many rows the enemy formation will have")]
    [SerializeField] int rows = 5;
    [Tooltip("How many columns the enemy formation will have")]    
    [SerializeField] int columns = 10;

    [Header("Prefabs")]
    [Tooltip("Enemy Prefabs to randomize go here")]
    [SerializeField] GameObject[] enemyPrefabs;

    void Awake()
    {
        SpawnRandomEnemies();
    }
    
    void Start()
    {
        StartMoving();
    }

    void SpawnRandomEnemies()   // Spawns a grid with 'rows' height and 'columns' width of random enemies
    {
        for (int row = rows; row > 0; row--)
        {
            int xPosition = -8;

            for (int column = columns; column > 0; column--)
            { 
                GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                Vector2 position = new Vector2(xPosition, row);
                var newEnemy = Instantiate(randomEnemy, position, Quaternion.identity);
                newEnemy.transform.parent = transform;      //  Makes the new Enemy Instance a child of this gameObject
                xPosition += 2;       // Moves to the next position in the row
            }
        }
    }

    void StartMoving()  //  Starts moving the enemy formation once every enemy has been Instantiated
    {
        GetComponent<MoveEnemy>().enabled = true;
    }
}
