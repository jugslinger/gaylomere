using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject back;
    public GameObject wall;
    public int level = 0;
    int backY = 0;
    int backX = 0;

    // Start is called before the first frame update
    void Start()
    {
        //TODO randomize back location and record location which will be used to Instantiate wall
        for (int i = 0; i < level + 10; i++)
        {
        int rand = Random.Range(0, level + 10);
        Instantiate(Sprite, transform.position, Quaternion.identity); //Places object https://docs.unity3d.com/ScriptReference/Object.Instantiate.html https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
