using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    public Text levelText;
    public Text player1Health;
    public Text player2Health;
    public Text player3Health;
    public Text player4Health;

    void Start()
    {
        switch (Global.playerCount)
        {
            case 1:
                player2Health.gameObject.SetActive(false);
                goto case 2;
            case 2:
                player3Health.gameObject.SetActive(false);
                goto case 3;
            case 3:
                player4Health.gameObject.SetActive(false);
                break;
        }
    }

    void Update()
    {
        levelText.text = "LEVEL: " + Global.level.ToString();
        player1Health.text = "Player 1:" + Global.player1CurrentHealth.ToString() + '/' + Global.player1MaxHealth.ToString();
        player2Health.text = "Player 2:" + Global.player2CurrentHealth.ToString() + '/' + Global.player2MaxHealth.ToString();
        player3Health.text = "Player 3:" + Global.player3CurrentHealth.ToString() + '/' + Global.player3MaxHealth.ToString();
        player4Health.text = "Player 4:" + Global.player4CurrentHealth.ToString() + '/' + Global.player4MaxHealth.ToString();
    }
}
