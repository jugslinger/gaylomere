using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour
{
    public GameObject back;
    public GameObject wall;
    public GameObject end;
    public GameObject begin;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public Camera cam;

    public List<Vector3> targets = new List<Vector3>();
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Global.playerAtBegin = 0;
        Global.playerAtEnd = 0;

        if (!Global.generated[Global.level])
            NewLevel();
        else
            ExistingLevel(); //Generates previously generated level


    }

        // Update is called once per frame
    void Update()
    {
        //checks that all players are at the beginning/end loads the next level
        if (Global.playerAtBegin >= Global.playerCount)
        {
            Global.level--;
            Global.side = true;
            SceneManager.LoadScene("SampleScene");
        }
        else if (Global.playerAtEnd >= Global.playerCount)
        {
            Global.level++;
            Global.side = false;
            SceneManager.LoadScene("SampleScene");
        }
    }

    void LateUpdate()
    {
        Vector3 centerPoint;
        float maxDist;

        targets.Insert(0, player1.transform.position);

        var bounds = new Bounds(targets[0], Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
                bounds.Encapsulate(targets[i]);

        if (targets.Count == 1)
            centerPoint = targets[0] + new Vector3(0, 0, -10);
        else
            centerPoint = bounds.center + new Vector3(0, 0, -10);

        if (bounds.size.x > bounds.size.y)
            maxDist = bounds.size.x;
        else
            maxDist = bounds.size.y;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, Mathf.Lerp(50f, 5000f, maxDist), Time.deltaTime);
        
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, centerPoint, ref velocity, 0.5f);
    }

    void NewLevel()
    {
        int size = Random.Range(Global.level + 10, Global.level + 15);

        int[] backY = new int[size + 2];
        int[] backX = new int[size + 2];
        int[] wallX = new int[99999];
        int[] wallY = new int[99999];

        int wallCount = 0;
        int rand = 1;

        //creates initial starting area
        backX[0] = 0;
        backY[0] = 0;
        Instantiate(back, new Vector3(backX[0], backY[0], 0), Quaternion.identity);
        if (Global.level > 1)
        {
            Instantiate(begin, new Vector3(backX[0], backY[0], 0), Quaternion.identity);
        }
        backX[1] = 2;
        backY[1] = 0;
        Instantiate(back, new Vector3(backX[1], backY[1], 0), Quaternion.identity);
        backX[2] = 4;
        backY[2] = 0;
        Instantiate(back, new Vector3(backX[2], backY[2], 0), Quaternion.identity);
        backX[3] = 6;
        backY[3] = 0;
        Instantiate(back, new Vector3(backX[3], backY[3], 0), Quaternion.identity);

        //spawns players if not previously generated
        switch (Global.playerCount)
        {
            case 4:
                Instantiate(player4, new Vector3(backX[2], backY[2], 0), Quaternion.identity);
                //targets.(player4.transform);
                goto case 3;
            case 3:
                Instantiate(player3, new Vector3(backX[1] + 1, backY[1], 0), Quaternion.identity);
                //targets.Add(player3.transform);
                goto case 2;
            case 2:
                Instantiate(player2, new Vector3(backX[1], backY[1], 0), Quaternion.identity);
                //targets.Add(player2.transform);
                goto default;
            default:
                Instantiate(player1, new Vector3(backX[1] - 1, backY[1], 0), Quaternion.identity);
                //targets.Add(player1.transform.position);
                break;
        }

        //loop creates base layout of map
        for (int i = 4; i < size - 3; i++)
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
        backX[size - 3] = backX[size - 4] + 2;
        backY[size - 3] = backY[size - 4];
        Instantiate(back, new Vector3(backX[size - 3], backY[size - 3], 0), Quaternion.identity);
        backX[size - 2] = backX[size - 3] + 2;
        backY[size - 2] = backY[size - 3];
        Instantiate(back, new Vector3(backX[size - 2], backY[size - 2], 0), Quaternion.identity);
        backX[size - 1] = backX[size - 2] + 2;
        backY[size - 1] = backY[size - 2];
        Instantiate(back, new Vector3(backX[size - 1], backY[size - 1], 0), Quaternion.identity);
        Instantiate(end, new Vector3(backX[size - 1], backY[size - 1], 0), Quaternion.identity);

        //loop that creates walls that surround map layout without overlapping other blocks
        for (int i = 0; i < size; i++)
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
        for (int i = 0; i < size; i++)
        {
            Global.backXStorage[Global.level, i] = backX[i];
            Global.backYStorage[Global.level, i] = backY[i];
        }
        Global.backCount[Global.level] = size;
        for (int i = 0; i < wallCount; i++)
        {
            Global.wallXStorage[Global.level, i] = wallX[i];
            Global.wallYStorage[Global.level, i] = wallY[i];
        }
        Global.wallCount[Global.level] = wallCount;
        Global.generated[Global.level] = true;
    }

    void ExistingLevel()
    {
        for (int i = 0; i < Global.backCount[Global.level]; i++)
        {
            Instantiate(back, new Vector3(Global.backXStorage[Global.level, i], Global.backYStorage[Global.level, i], 0), Quaternion.identity);
        }

        if (Global.level > 1)
        {
            Instantiate(begin, new Vector3(Global.backXStorage[Global.level, 0], Global.backYStorage[Global.level, 0], 0), Quaternion.identity);
        }

        if (Global.side)
        {
            switch (Global.playerCount)
            {
                case 4:
                    Instantiate(player4, new Vector3(Global.backXStorage[Global.level, Global.backCount[Global.level] - 1], Global.backYStorage[Global.level, Global.backCount[Global.level] - 1], 0), Quaternion.identity);
                    //targets.Add(player4.transform);
                    goto case 3;
                case 3:
                    Instantiate(player3, new Vector3(Global.backXStorage[Global.level, Global.backCount[Global.level] - 1] - 1, Global.backYStorage[Global.level, Global.backCount[Global.level] - 1], 0), Quaternion.identity);
                    //targets.Add(player3.transform);
                    goto case 2;
                case 2:
                    Instantiate(player2, new Vector3(Global.backXStorage[Global.level, Global.backCount[Global.level] - 1] - 2, Global.backYStorage[Global.level, Global.backCount[Global.level] - 1], 0), Quaternion.identity);
                    //targets.Add(player2.transform);
                    goto default;
                default:
                    Instantiate(player1, new Vector3(Global.backXStorage[Global.level, Global.backCount[Global.level] - 1] - 3, Global.backYStorage[Global.level, Global.backCount[Global.level] - 1], 0), Quaternion.identity);
                    //targets.Add(player1.transform);
                    break;
            }
        }
        else
        {
            switch (Global.playerCount)
            {
                case 4:
                    Instantiate(player4, new Vector3(Global.backXStorage[Global.level, 2], Global.backYStorage[Global.level, 2], 0), Quaternion.identity);
                    //targets.Add(player4.transform);
                    goto case 3;
                case 3:
                    Instantiate(player3, new Vector3(Global.backXStorage[Global.level, 1] + 1, Global.backYStorage[Global.level, 1], 0), Quaternion.identity);
                    //targets.Add(player3.transform);
                    goto case 2;
                case 2:
                    Instantiate(player2, new Vector3(Global.backXStorage[Global.level, 1], Global.backYStorage[Global.level, 1], 0), Quaternion.identity);
                    //targets.Add(player2.transform);
                    goto default;
                default:
                    Instantiate(player1, new Vector3(Global.backXStorage[Global.level, 1] - 1, Global.backYStorage[Global.level, 1], 0), Quaternion.identity);
                    //targets.Add(player1.transform);
                    break;
            }
        }

        Instantiate(end, new Vector3(Global.backXStorage[Global.level, Global.backCount[Global.level] - 1], Global.backYStorage[Global.level, Global.backCount[Global.level] - 1], 0), Quaternion.identity);

        for (int i = 0; i < Global.wallCount[Global.level]; i++)
        {
            Instantiate(wall, new Vector3(Global.wallXStorage[Global.level, i], Global.wallYStorage[Global.level, i], 0), Quaternion.identity);
        }
    }

}