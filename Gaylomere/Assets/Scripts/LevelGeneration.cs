using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour
{
    public GameObject back;
    public GameObject wall;
    public GameObject end;
    int rand = 1;

    // Start is called before the first frame update
    void Start()
    {
        int size = Random.Range(Global.level + 5, Global.level + 10);

        int[,] backY = new int[999999, size + 2];
        int[,] backX = new int[999999, size + 2];
        int[,] wallX = new int[999999, 999999];
        int[,] wallY = new int[999999, 999999];

        Global.playerAtEnd = 0;
        int wallCount = 0;

        //creates initial starting area
        backY[Global.level, 0] = 0;
        backX[Global.level, 0] = 0;
        Instantiate(back, new Vector3(backX[Global.level, 0], backY[Global.level, 0], 0), Quaternion.identity);
        backY[Global.level, 1] = 0;
        backX[Global.level, 1] = 1;
        Instantiate(back, new Vector3(backX[Global.level, 1], backY[Global.level, 1], 0), Quaternion.identity);

        //loop creates base layout of map
        for (int i = 2; i <= size; i++)
        {
            if (rand == 0)
                rand = Random.Range(0, 2);
            else if (rand == 1)
                rand = Random.Range(0, 3);
            else
                rand = Random.Range(1, 3);

            if (rand == 0) //up
            {
                backX[Global.level, i] = backX[Global.level, i - 1];
                backY[Global.level, i] = backY[Global.level, i - 1] + 1;
            }
            else if (rand == 1) //right
            {
                backX[Global.level, i] = backX[Global.level, i - 1] + 1;
                backY[Global.level, i] = backY[Global.level, i - 1];
            }
            else //down
            {
                backX[Global.level, i] = backX[Global.level, i - 1];
                backY[Global.level, i] = backY[Global.level, i - 1] - 1;
            }

            Instantiate(back, new Vector3(backX[Global.level, i], backY[Global.level, i], 0), Quaternion.identity);
        }

        //creates a level ending area
        Instantiate(back, new Vector3(backX[Global.level, size] + 1, backY[Global.level, size], 0), Quaternion.identity);
        Instantiate(end, new Vector3(backX[Global.level, size] + 1, backY[Global.level, size], 0), Quaternion.identity);
        backX[Global.level, size + 1] = backX[Global.level, size] + 1;
        backY[Global.level, size + 1] = backY[Global.level, size];

        //loop that creates walls that surround map layout without overlapping other blocks
        for(int i = 0; i < size + 2; i++)
            for (int j = -1; j < 2; j++)
                for (int k = -1; k < 2; k++)
                {
                    bool exist = false;
                    for (int m = 0; m < size + 2 && !exist; m++)
                        if (backX[Global.level, i] + j == backX[Global.level, m] && backY[Global.level, i] + k == backY[Global.level, m])
                            exist = true;
                    for (int n = 0; n <= wallCount && !exist; n++)
                        if (backX[Global.level, i] + j == wallX[Global.level, n] && backY[Global.level, i] + k == wallY[Global.level, n])
                            exist = true;
                    if (!exist)
                    {
                        Instantiate(wall, new Vector3(backX[Global.level, i] + j, backY[Global.level, i] + k, 0), Quaternion.identity);
                        wallX[Global.level, wallCount] = backX[Global.level, i] + j;
                        wallY[Global.level, wallCount] = backY[Global.level, i] + k;
                        wallCount++;
                    }
                }

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
