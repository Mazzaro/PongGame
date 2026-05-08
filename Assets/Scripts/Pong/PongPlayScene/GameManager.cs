using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform playerPaddle;
    public Transform enemyPaddle;
    public BallController ballController;

    public int playerScore = 0;
    public int enemyScore = 0;
    public int winPoints;
    
    public TextMeshProUGUI textPointsPlayer;
    public TextMeshProUGUI textPointsEnemy;
    public TextMeshProUGUI textNameP1;
    public TextMeshProUGUI textNameP2;
    public TextMeshProUGUI textEndGame;

    public TextMeshProUGUI textFinalScore;

    public GameObject screenEndGame;

    public GameObject pausePanel;
     private bool isPaused = false;
    
    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isPaused)
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;
        //Define as posições iniciais das raquetes
        playerPaddle.position = new Vector3(7f, 0f, 0f);
        enemyPaddle.position = new Vector3(-7f, 0f, 0f);

        ballController.ResetBall();

        playerScore = 0;
        enemyScore = 0;

        textPointsEnemy.text = enemyScore.ToString();
        textPointsPlayer.text = playerScore.ToString();

        textNameP1.text = SaveController.Instance.nameEnemy;
        textNameP2.text = SaveController.Instance.namePlayer;
    }

    public void ScorePlayer()
    {
        playerScore++;
        textPointsPlayer.text = playerScore.ToString();
        CheckWin();
    }

    public void ScoreEnemy()
    {
        enemyScore++;
        textPointsEnemy.text = enemyScore.ToString();
        CheckWin();
    }

    public void CheckWin()
    {
        if(enemyScore >= winPoints || playerScore >= winPoints)
        {
            //ResetGame();
            EndGame();
        }
    }
    public void EndGame()
    {
        
        screenEndGame.SetActive(true);

        bool playerWon = playerScore > enemyScore;

        string winnerName;
        string loserName;

        int winnerScore;
        int loserScore;

        if (playerWon)
        {
            winnerName = SaveController.Instance.GetName(true);
            loserName = SaveController.Instance.GetName(false);

            winnerScore = playerScore;
            loserScore = enemyScore;
        }
        else
        {
            winnerName = SaveController.Instance.GetName(false);
            loserName = SaveController.Instance.GetName(true);

            winnerScore = enemyScore;
            loserScore = playerScore;
        }

        textEndGame.text = "Winner: " + winnerName;

        textFinalScore.text = winnerName + " " + winnerScore + " X " + loserScore + " " + loserName;

        SaveController.Instance.SaveWinner(winnerName);
        SaveController.Instance.SaveScores(textFinalScore.text);
        ResetGame();

        Invoke("LoadMenu", 3f);

    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
