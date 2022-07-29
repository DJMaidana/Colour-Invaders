using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int rows = 5;
    [SerializeField] int columns = 10;

    void Start()
    {
        SpawnRandomEnemies();
        StartMoving();
    }

    void SpawnRandomEnemies()
    {
        for (int row = rows; row > 0; row--)
        {
            int xPosition = -8;

            for (int column = columns; column > 0; column--)
            { 
                GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                Vector2 position = new Vector2(xPosition, row);
                var newEnemy = Instantiate(randomEnemy, position, Quaternion.identity);
                newEnemy.transform.parent = transform;
                xPosition += 2;
            }
        }
    }

    void StartMoving()
    {
        GetComponent<MoveEnemy>().enabled = true;
    }
}
