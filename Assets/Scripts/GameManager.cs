using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameDifficulty = 1;
    [SerializeField] float difficultyIncrease = .1f;

    MoveEnemy enemyMover;

    void Start()
    {
        enemyMover = FindObjectOfType<MoveEnemy>();
    }

    public void IncreaseDifficulty()
    {
        enemyMover.lateralSpeed += difficultyIncrease;
    }
}
