
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Dropdown playerCountDropdown;
    int tempNum;

   void Start()
    {
        List<string> numText = new List<string>() { "1", "2", "3", "4" };
        playerCountDropdown.AddOptions(numText); //populates dropdwon on menu
    }

    void Update()
    {
        tempNum = playerCountDropdown.GetComponent<Dropdown>().value + 1;
    }

    public void DropdownChanged (int n)
    {
        tempNum = n + 1;
    }

    public void PlayGame ()
    {
        Global.playerCount = tempNum;
        SceneManager.LoadScene("SampleScene");
    }
}