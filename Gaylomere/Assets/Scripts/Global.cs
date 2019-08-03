using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static int playerCount = 1;

    public static int level = 1;
    public static int playerAtBegin = 0;
    public static int playerAtEnd = 0;
    public static bool side = false;

    public static int[,] backXStorage = new int[9999, 9999];
    public static int[,] backYStorage = new int[9999, 9999];
    public static int[] backCount = new int[9999];
    public static int[,] wallXStorage = new int[9999, 9999];
    public static int[,] wallYStorage = new int[9999, 9999];
    public static int[] wallCount = new int[9999];
    public static bool[] generated = new bool[9999];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
