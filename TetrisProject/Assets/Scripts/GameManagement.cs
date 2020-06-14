using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    // スコア関連
    public Text scoreText;

    private int score;
    public int clearScore = 200;

    // タイマー関連
    public Text timerText;

    public float gameTime = 60f;
    int seconds;

    // ゲームクリア、ゲームオーバー関連
    public GameObject gameOverUI;
    public SceneFader sceneFader;

    public string menuSceneName = "Menu";



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

        // スコアを表示する
        scoreText.text = score.ToString();

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
            //GameClear();
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
        Toggle();
    }

    public void Clear()
    {
        Debug.Log("GameClear");
        SceneManager.LoadScene("SampleScene");
    }

    
    public void Toggle()
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

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

}
