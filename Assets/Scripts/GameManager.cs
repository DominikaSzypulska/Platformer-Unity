using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
        
    public enum GameState {GAME, GAME_OVER, PAUSE_MENU, GS_LEVEL_COMPLETED, OPTIONS}

    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text enemiesText;
    public TMP_Text endScoreText;
    public TMP_Text  highScoreText;
    public int score = 0;
    public static GameManager instance;
    public Image[] keysTab;
    private int keysFound = 0;
    public Image[] livesTab;
    private int lives = 3;
    private int enemies = 0;
    public GameState currentGameState=GameState.GAME;
    public bool isInGame = false;
    public Canvas inGameCanvas;
    public Canvas pauseMenuCanvas;
    public Canvas lebelComletedCanvas;
    public Canvas OptionCanvas;
    private static string keyHighScore = "HighScoreLevel1";
    int highScore;
    public float timer = 0;

    public AudioListener audioListener;
    // Start is called before the first frame update
    void Start()
    {
        int highScore = PlayerPrefs.GetInt(keyHighScore);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.GAME)
        {
            timer = timer + Time.deltaTime;
            string formattedTime = string.Format("{0:00}:{1:00}", timer / 60, timer % 60);
            timeText.text = formattedTime;
            
                      
            
        }
        
       
        
        

        
    }

    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void SetVolume(float vol)
    {
        AudioListener.volume=vol;
       
    }
    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnReturnToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QualityIncrease()
    {
        QualitySettings.IncreaseLevel();
    }
    public void QualityDecrease()
    {
        QualitySettings.DecreaseLevel();
    }
    public void optionsMenu()
    {
        Options();
    }
    public void AddPoints(int points)
    {
        score = score + points;
        scoreText.text = score.ToString();
    }
    public void killEnemies(int enemiesNumber)
    {
        enemies += enemiesNumber;
        enemiesText.text = enemies.ToString();
    }

    public void AddLives(int live)
    {
        lives += live;
        livesTab[lives-1 ].enabled = true;
    }

    public void OddLives(int live)
    {
        lives -= live;
        livesTab[lives ].enabled = false;
    }
    public void AddKeys(int keyID)
    {
        keysFound++;
        keysTab[keysFound-1].color=Color.red;
    }
    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        
    
        if (newGameState == GameState.GAME)
        {OptionCanvas.enabled = false;
            isInGame = true;
            inGameCanvas.enabled = true;
            pauseMenuCanvas.enabled = false;
            lebelComletedCanvas.enabled = false;

        }
        else if(newGameState == GameState.PAUSE_MENU)
        {OptionCanvas.enabled = false;
            isInGame = false;
            inGameCanvas.enabled = false;
            pauseMenuCanvas.enabled = true;
            lebelComletedCanvas.enabled = false;
        }
        else if(newGameState == GameState.GS_LEVEL_COMPLETED)
        {OptionCanvas.enabled = false;
            isInGame = false;
            inGameCanvas.enabled = false;
            pauseMenuCanvas.enabled = false;
            lebelComletedCanvas.enabled = true;
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                
                if (highScore < score)
                {
                    highScore = score;
                    
                }
                
            }
            endScoreText.text = "Your score: "+ score.ToString();
            highScoreText.text = "The highest score: " + highScore.ToString();   

        } 
        
        else if(newGameState == GameState.OPTIONS)
        {
            OptionCanvas.enabled = true;
            isInGame = false;
            inGameCanvas.enabled = false;
            pauseMenuCanvas.enabled = false;
            lebelComletedCanvas.enabled = false;
        }
        else
        {   OptionCanvas.enabled = false;
            isInGame = false;
            inGameCanvas.enabled = false;
            pauseMenuCanvas.enabled = false;
            lebelComletedCanvas.enabled = false;
        }
    
        
    }
    public void PauseMenu()
    {
        SetGameState(GameState.PAUSE_MENU);
    }
    public void LevelComleted()
    {
        SetGameState(GameState.GS_LEVEL_COMPLETED);
    }
    public void InGame()
    {
        
        SetGameState(GameState.GAME);
        
    }
    public void GameOver()
    {
        SetGameState(GameState.GAME_OVER);
    }

    public void Options()
    {
        SetGameState(GameState.OPTIONS);
        
    }

    void Awake()
    {   
        scoreText.text = score.ToString();
        timeText.text = timer.ToString();
        enemiesText.text = enemies.ToString();
        PlayerPrefs.SetInt(keyHighScore,score);
        InGame();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicated Game Manager", gameObject);
        }

        if (!PlayerPrefs.HasKey(keyHighScore))
        {
            PlayerPrefs.SetInt(keyHighScore,0);
        }
        for (int i = 0; i < keysTab.Length; i++)
        {
            keysTab[i].color = Color.grey;
        }
        for (int i = 3; i < livesTab.Length; i++)
        {
            livesTab[i].enabled = false;
        }
    }
}
