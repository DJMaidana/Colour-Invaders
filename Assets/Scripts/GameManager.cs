using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameDifficulty = 1;
    [SerializeField] float difficultyIncrease = .1f;
    public int enemiesConnected = 0;

    [SerializeField] float currentScore;
    float hiScore;

    public List<Enemy> listEnemiesConnected = new List<Enemy>();

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

        CalculateScore();

        listEnemiesConnected.Clear();
    }

    public void IncreaseDifficulty()
    {
        enemyMover.lateralSpeed += difficultyIncrease;
    }

    void CalculateScore()
    {
        int enemiesDestroyed = listEnemiesConnected.Count;
        float scoreAwarded = enemiesDestroyed * Fibonacci(enemiesDestroyed + 1) * 10;

        currentScore += scoreAwarded;
    }

    int Fibonacci(int n)  
    {  
        if ((n == 0) || (n == 1))  
        {  
            return n;  
        }  
        else
        {  
            return Fibonacci(n - 1) + Fibonacci(n - 2);  
        }
    }  

}
