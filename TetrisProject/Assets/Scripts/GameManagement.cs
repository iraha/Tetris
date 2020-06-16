using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    // TetrisBlock クラスを呼び出す
    //public TetrisBlock tetrisBlock;


    // スコア関連
    public Text scoreText;

    private int score;
    public int clearScore = 200;

    // タイマー関連
    public Text timerText;

    public float gameTime = 60f;
    int seconds;

    // ゲームクリア、ゲームオーバー関連
    public SceneFader sceneFader;

    public GameObject gameOverUI;
    public GameObject gameClearUI;



    // スコアの点数
    public int scoreOneLine = 100;
    public int scoreTwoLine = 250;
    public int scoreThreeLine = 500;
    public int scoreFourLine = 1000;

    private int currentScore = 0;

    private int numberOfLines;


    // Start is called before the first frame update
    void Start()
    {


        // スコアを0に戻す
        Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        
        TimeManagement();
        UpdateScore();
        
        // スコアを表示する
        //AddScore();

    }

    public void UpdateScoreUI() 
    {
        scoreText.text = currentScore.ToString();
    }

    // スコア関連
    public void UpdateScore()
    {
        if (numberOfLines > 0)
        {
            if (numberOfLines == 1)
            {
                OneLine();
            }
            else if (numberOfLines == 2)
            {
                TwoLine();
            }
            else if (numberOfLines == 3)
            {
                ThreeLine();
            }
            else if (numberOfLines == 4)
            {
                FourLine();
            }
            numberOfLines = 0;
        }
    }

    // １列がそろっと場合のスコア
    public void OneLine()
    {
        currentScore += scoreOneLine;
    }
    // ２列がそろっと場合のスコア
    public void TwoLine()
    {
        currentScore += scoreTwoLine;
    }
    // ３列がそろっと場合のスコア
    public void ThreeLine()
    {
        currentScore += scoreThreeLine;
    }
    // ４列がそろっと場合のスコア
    public void FourLine()
    {
        currentScore += scoreFourLine;
    }

    // ゲーム開始前の状態に戻す
    private void Initialize()
    {
        // スコアを0に戻す
        score = 0;

    }


    // スコアの追加
    public void AddScore()
    {
        score += 50;
        Debug.Log("Add 50");

        if (score == clearScore)
        {
            Debug.Log(clearScore);
            Clear();
        }
    }
    

    // タイマーの設定
    public void TimeManagement()
    {

        gameTime -= Time.deltaTime;
        seconds = (int)gameTime;
        timerText.text = seconds.ToString();

        if (seconds == 0)
        {
            Debug.Log("TimeOut");
            Die();
            //GameClear();
            //FindObjectOfType<GameOver>().EndGame();
        }
    }
    
    public void Die()
    {
        Debug.Log("GameOver");
        GameOverToggle();
    }

    public void Clear()
    {
        Debug.Log("GameClear");
        GameClearToggle();
        //SceneManager.LoadScene("SampleScene");
    }

    
    public void GameOverToggle()
    {
        gameOverUI.SetActive(!gameOverUI.activeSelf);

        if (gameOverUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GameClearToggle()
    {
        gameClearUI.SetActive(!gameClearUI.activeSelf);

        if (gameClearUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        //SceneManager.LoadScene("SampleScene");
        //sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        Debug.Log("Retry");
    }

    public void Menu()
    {
        //sceneFader.FadeTo(menuSceneName);
    }

}
