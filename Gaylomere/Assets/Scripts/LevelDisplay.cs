using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    public Text levelText;
    void Update()
    {
        levelText.text = "LEVEL: " + Global.level.ToString();
    }
}
