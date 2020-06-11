using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{

    public Text scoreText;

    private int score;
    public int clearScore = 200;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    private void Initialize() 
    {
        score = 0;
    }

    public void AddScore() 
    {
        score += 50;

        if (score == clearScore) 
        {
            //Debug.Log("GameClear");
            FindObjectOfType<TetrisBlock>().GameOver();
        }
    }

}
