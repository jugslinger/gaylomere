﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour
{
    public GameObject back;
    public GameObject wall;
    public GameObject end;
    public GameObject begin;
    public GameObject player;
    int rand = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (!Global.generated[Global.level])
        {
            int size = Random.Range(Global.level + 5, Global.level + 10);

            int[] backY = new int[size + 2];
            int[] backX = new int[size + 2];
            int[] wallX = new int[99999];
            int[] wallY = new int[99999];

            Global.playerAtEnd = 0;
            int wallCount = 0;

            //creates initial starting area
            backY[0] = 0;
            backX[0] = 0;
            Instantiate(back, new Vector3(backX[0], backY[0], 0), Quaternion.identity);
            Instantiate(begin, new Vector3(backX[0], backY[0], 0), Quaternion.identity);
            backY[1] = 0;
            backX[1] = 2;
            Instantiate(back, new Vector3(backX[1], backY[1], 0), Quaternion.identity);
            Instantiate(player, new Vector3(backX[1], backY[1], 0), Quaternion.identity);
            backY[2] = 0;
            backX[2] = 4;
            Instantiate(back, new Vector3(backX[2], backY[2], 0), Quaternion.identity);

            //loop creates base layout of map
            for (int i = 3; i < size; i++)
            {
                if (rand == 0)
                    rand = Random.Range(0, 2);
                else if (rand == 1)
                    rand = Random.Range(0, 3);
                else
                    rand = Random.Range(1, 3);

                if (rand == 0) //up
                {
                    backX[i] = backX[i - 1];
                    backY[i] = backY[i - 1] + 2;
                }
                else if (rand == 1) //right
                {
                    backX[i] = backX[i - 1] + 2;
                    backY[i] = backY[i - 1];
                }
                else //down
                {
                    backX[i] = backX[i - 1];
                    backY[i] = backY[i - 1] - 2;
                }

                Instantiate(back, new Vector3(backX[i], backY[i], 0), Quaternion.identity);
            }

            //creates a level ending area
            backX[size] = backX[size - 1] + 2;
            backY[size] = backY[size - 1];
            Instantiate(back, new Vector3(backX[size], backY[size], 0), Quaternion.identity);
            Instantiate(end, new Vector3(backX[size], backY[size], 0), Quaternion.identity);

            //loop that creates walls that surround map layout without overlapping other blocks
            for (int i = 0; i <= size; i++)
                for (int j = -2; j < 3; j += 2)
                    for (int k = -2; k < 3; k += 2)
                    {
                        bool exist = false;
                        for (int m = 0; m <= size && !exist; m++)
                            if (backX[i] + j == backX[m] && backY[i] + k == backY[m])
                                exist = true;
                        for (int n = 0; n <= wallCount && !exist; n++)
                            if (backX[i] + j == wallX[n] && backY[i] + k == wallY[n])
                                exist = true;
                        if (!exist)
                        {
                            Instantiate(wall, new Vector3(backX[i] + j, backY[i] + k, 0), Quaternion.identity);
                            wallX[wallCount] = backX[i] + j;
                            wallY[wallCount] = backY[i] + k;
                            wallCount++;
                        }
                    }

            //level storage for return
            for (int i = 0; i <= size; i++)
            {
                Global.backXStorage[Global.level, i] = backX[i];
                Global.backYStorage[Global.level, i] = backY[i];
            }
            for (int i = 0; i < wallCount; i++)
            {
                Global.wallXStorage[Global.level, i] = wallX[i];
                Global.wallYStorage[Global.level, i] = wallY[i];
            }
            Global.generated[Global.level] = true;
        }
        else
            Debug.Log("No");
    }

    // Update is called once per frame
    void Update()
    {
        //checks that all players are at the and loads the next level
        if (Global.playerAtEnd >= 1)
        {
            Global.level++;
            SceneManager.LoadScene("SampleScene");
        }
    }
}
