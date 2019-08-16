using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static int playerCount = 2;

    public static int player1MaxHealth = 10;
    public static int player1CurrentHealth = 10;
    public static int player2MaxHealth = 10;
    public static int player2CurrentHealth = 10;
    public static int player3MaxHealth = 10;
    public static int player3CurrentHealth = 10;
    public static int player4MaxHealth = 10;
    public static int player4CurrentHealth = 10;

    public static int level = 1;
    public static int playerAtBegin = 0;
    public static int playerAtEnd = 0;
    public static bool side = false;

    public static int[,] backXStorage = new int[10000, 10000];
    public static int[,] backYStorage = new int[10000, 10000];
    public static int[] backCount = new int[10000];
    public static int[,] wallXStorage = new int[10000, 10000];
    public static int[,] wallYStorage = new int[10000, 10000];
    public static int[] wallCount = new int[10000];
    public static bool[] generated = new bool[10000];

    public static int[] enemyCount = new int[10000];
    public struct enemyStat
    {
        public static bool isAlive = true;
        public static int enemyXStorage;
        public static int enemyYStorage;
        public static int health;
    }
    public static enemyStat[,] enemyStats = new enemyStat[10000, 10000];

}
