using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Places object https://docs.unity3d.com/ScriptReference/Object.Instantiate.html https://docs.unity3d.com/Manual/InstantiatingPrefabs.html

public class LevelGeneration : MonoBehaviour
{
    public GameObject back;
    public GameObject wall;
    public int level = 0;
    int rand = 1;

    // Start is called before the first frame update
    void Start()
    {
        level++;
        int size = Random.Range(level, level + 10);

        int[] backY = new int[size + 2];
        int[] backX = new int[size + 2];
        int[] wallX = new int[999999];
        int[] wallY = new int[999999];

        int wallCount = 0;

        //creates initial starting area
        backY[0] = 0;
        backX[0] = 0;
        Instantiate(back, new Vector3(backX[0], backY[0], 0), Quaternion.identity);
        backY[1] = 0;
        backX[1] = 1;
        Instantiate(back, new Vector3(backX[1], backY[1], 0), Quaternion.identity);

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
                backX[i] = backX[i - 1];
                backY[i] = backY[i - 1] + 1;
            }
            else if (rand == 1) //right
            {
                backX[i] = backX[i - 1] + 1;
                backY[i] = backY[i - 1];
            }
            else //down
            {
                backX[i] = backX[i - 1];
                backY[i] = backY[i - 1] - 1;
            }

            Instantiate(back, new Vector3(backX[i], backY[i], 0), Quaternion.identity);
        }

        //creates a level ending area
        Instantiate(back, new Vector3(backX[size] + 1, backY[size], 0), Quaternion.identity);
        backX[size + 1] = backX[size] + 1;
        backY[size + 1] = backY[size];

        //FIX loop that creates walls that surround map layout without overlapping other blocks
        for(int i = 0; i < size + 2; i++)
            for (int j = -1; j < 2; j++)
                for (int k = -1; k < 2; k++)
                {
                    bool exist = false;
                    for (int m = 0; m < size + 2 && !exist; m++)
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
