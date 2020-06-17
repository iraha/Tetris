using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TetrisBlock : MonoBehaviour
{

    public Vector3 rotationPoint;
    private float previousTime;
    [SerializeField] public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;

    private static Transform[,] grid = new Transform[width, height];


    // ラインの数
    private int numberOfLines = 0;


    void Start() 
    {

    }

    void LateUpdate()
    {
        Movement();
        //AddScore();
    }

    


    private void Movement()
    {
        // ブロックの左への動き
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove()) 
                transform.position -= new Vector3(-1, 0, 0);
            
        } // ブロックの右への動き
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            // ブロックの回転
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }

        // ブロックが下に行く時間
        else if ((Input.GetKey(KeyCode.DownArrow) || Time.time - previousTime >= fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove()) 
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnBlock>().NewBlock();

            }
            previousTime = Time.time;
        }
    }


    public void CheckForLines() 
    {
        for (int i = height - 1; i >= 0; i--) 
        {
           if (HasLine(i)) 
           {
                DeleteLine(i);
                RowDown(i);
                
           } 
        }
    }

    // 列がそろっているか確認
    bool HasLine(int i) 
    {
        for(int j = 0; j < width; j++) 
        {
            if (grid[j, i] == null)
            return false;
        }
        // ラインの数を数える
        numberOfLines++;
        Debug.Log(numberOfLines);

        FindObjectOfType<GameManagement>().AddScore();
        //score += 100;
        //print(score);
        return true;
    }

    // ラインを消す
    void DeleteLine(int i) 
    {
        for (int j = 0; j < width; j++) 
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }

        //Debug.Log(numberOfLines);
    }
    
    // 列を下げる
    public void RowDown(int i) 
    {
        for (int y = i; y < height; y++) 
        {
            for (int j = 0; j < width; j++) 
            {
                if(grid[j, y] != null) 
                {
                    grid[j, y - 1] = grid[j,y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }


    public void AddToGrid() 
    {
        foreach (Transform children in transform) 
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;

            // height-1 = 19のところまでブロックがきたらGameOver
            if (roundedY >= height-1) 
            {
                FindObjectOfType<GameManagement>().GameOver();
                //GameOver();
            }

        }
    }

    // ブロック移動の制御
    bool ValidMove() 
    {

        foreach (Transform children in transform) 
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            // ブロックがステージよりはみ出さないように制御
            if (roundedX < 0 || roundedX >= width || roundedY < 0 ||roundedY >= height) 
            {
                return false;
            }
            
            if (grid[roundedX, roundedY] != null) 
            {
                return false;
            }

        }
        return true;
    }

    


}
