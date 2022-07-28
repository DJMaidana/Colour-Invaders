using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameDifficulty = 1;
    [SerializeField] float difficultyIncrease = .1f;
    public int enemiesConnected = 0;

    public List<Enemy> listEnemiesConnected = new List<Enemy>();
    public List<GameObject> enemiesToDestroy = new List<GameObject>();

    MoveEnemy enemyMover;

    void Start()
    {
        enemyMover = FindObjectOfType<MoveEnemy>();
    }

    public void GetConnectedEnemies()
    {
        Enemy currentEnemy = listEnemiesConnected[0];
        currentEnemy.CheckNeighbors();
        
        foreach (Enemy enemy in listEnemiesConnected)
        {
            enemy.EnemyDeath();
        }

        listEnemiesConnected.Clear();
    }

    public void IncreaseDifficulty()
    {
        enemyMover.lateralSpeed += difficultyIncrease;
    }
}
