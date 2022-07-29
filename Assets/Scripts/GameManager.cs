using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameDifficulty = 1f;
    float difficultyIncrease = .1f;

    int currentScore;
    float hiScore;
    [SerializeField] int enemiesRemaining;

    public List<Enemy> listEnemiesConnected = new List<Enemy>();

    MoveEnemy enemyMover;
    UIManager canvas;
    PlayerController player;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        canvas = FindObjectOfType<UIManager>();

        if (!PlayerPrefs.HasKey("CurrentScore"))
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
            PlayerPrefs.SetInt("HiScore", 0);
            PlayerPrefs.SetInt("Lives", 3);
        }
        else
        {
            LoadScores();
            LoadLives();
        }
    }

    void Start()
    {
        enemyMover = FindObjectOfType<MoveEnemy>();
        enemiesRemaining = FindObjectsOfType<Enemy>().Length;

    }

    void Update()
    {
        if (enemiesRemaining <= 0)
        {
            Win();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackToMenu();
        }
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
        CalculateRemainingEnemies();
        listEnemiesConnected.Clear();
    }

    public void IncreaseDifficulty()
    {
        enemyMover.lateralSpeed += difficultyIncrease;
    }

    void CalculateScore()
    {
        int enemiesDestroyed = listEnemiesConnected.Count;
        int scoreAwarded = enemiesDestroyed * Fibonacci(enemiesDestroyed + 1) * 10;

        currentScore += scoreAwarded;

        SetCanvasScore();
    }   

    void CalculateRemainingEnemies()
    {
        enemiesRemaining -= listEnemiesConnected.Count;
    }

    void SetCanvasScore()
    {
        string format = "0000000";
        string currentScoreString = currentScore.ToString(format);
        string hiScoreString = hiScore.ToString(format);
        canvas.currentScoreText.text = $"SCORE - {currentScoreString}";
        canvas.hiScoreText.text = $"HI-SCORE - {hiScoreString}";
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

    public void Win()
    {
        CheckHiScore();
        SaveCurrentScore();
        SaveLives();
        //Canvas level won enable
        Invoke("ReloadLevel", 2f);
    }

    public void GameOver()
    {
        CheckHiScore();
        ResetCurrentScore();
        ResetLives();
        //canvas gameover enable
        Invoke("ReloadLevel", 2f);
    }

    void SaveCurrentScore()
    {
        PlayerPrefs.SetInt("CurrentScore", currentScore);
    }

    void ResetCurrentScore()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    void LoadScores()
    {
        currentScore = PlayerPrefs.GetInt("CurrentScore");
        hiScore = PlayerPrefs.GetInt("HiScore");
        SetCanvasScore();
    }

    void CheckHiScore()
    {
        if (currentScore > hiScore)
        {
            PlayerPrefs.SetInt("HiScore", currentScore);
        }
    }

    void LoadLives()
    {
        player.hitPoints = PlayerPrefs.GetInt("Lives");
        SetLivesImages();
    }

    void SaveLives()
    {
        int remainingLives = player.hitPoints;
        PlayerPrefs.SetInt("Lives", remainingLives);
    }

    void ResetLives()
    {
        PlayerPrefs.SetInt("Lives", 3);
    }

    void SetLivesImages()
    {
        int imageIndex = 2;

        while (imageIndex != player.hitPoints - 1)
        {
            canvas.livesImages[imageIndex].enabled = false;
            imageIndex--;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
