using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    [Header("Variables")]
    [Tooltip("Enemy movement speed at start")]
    public float gameDifficulty = 1f;
    float difficultyIncrease = .1f;     //  How much enemies increase in speed for each enemy destroyed
    [SerializeField] int enemiesRemaining;

    int currentScore;
    float hiScore;
    
    public List<Enemy> listEnemiesConnected = new List<Enemy>();

    MoveEnemy enemyMover;
    UIManager canvas;
    PlayerController player;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        canvas = FindObjectOfType<UIManager>();

        if (!PlayerPrefs.HasKey("CurrentScore"))        // If there is no previously saved data, creates it 
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
            PlayerPrefs.SetInt("HiScore", 0);
            PlayerPrefs.SetInt("Lives", 3);
        }
        else        // If there is saved data, loads it
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
        if (enemiesRemaining <= 0)      // When there are no more enemies, starts Win Sequence
        {
            Win();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackToMenu();
        }
    }

    //  Makes enemies hit by a Player Bullet check their neighbors' color in a recursive way
    //  Adds all the same colored neighbours to a List and uses it to calculate score
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

    void CalculateScore()   // Calculates score to be awarded based on amount of enemies destroyed
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

    public void ReduceRemainingEnemies(int amountToReduce)
    {
        enemiesRemaining -= amountToReduce;
    }

    void SetCanvasScore()   // Updates canvas scores
    {
        string format = "0000000";
        string currentScoreString = currentScore.ToString(format);
        string hiScoreString = hiScore.ToString(format);
        canvas.currentScoreText.text = $"SCORE - {currentScoreString}";
        canvas.hiScoreText.text = $"HI-SCORE - {hiScoreString}";
    }

    int Fibonacci(int n)    // Calculates the Nth fibonacci number
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
        ShowWinMessage();
        Invoke("ReloadLevel", 2f);
    }

    public void GameOver()
    {
        CheckHiScore();
        ResetCurrentScore();
        ResetLives();
        ShowGameOverMessage();
        Invoke("ReloadLevel", 2f);
    }

    void ShowWinMessage()
    {
        canvas.levelCompleteText.gameObject.SetActive(true);
    }

    void ShowGameOverMessage()
    {
        canvas.gameOverText.gameObject.SetActive(true);
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

    void SetLivesImages()   // Updates how many lives are shown based on the Player's remaining lives
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
