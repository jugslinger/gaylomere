using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    Rigidbody2D rb;
    private float Speed = 5f;
    private Vector3 input;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the rigidbody component added to the object and store it in rb
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0) * Speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 200, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D col) // col is the trigger object we collided with
    {
        if (col.tag == "Coin")
        {
            Destroy(col.gameObject); // remove the coin
        }
        else if (col.tag == "Begin")
        {
            Global.playerAtBegin++;
        }
        else if (col.tag == "End")
        {
            Global.playerAtEnd++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "End")
        {
            Global.playerAtEnd--;
        }
        else if (col.tag == "Begin")
        {
            Global.playerAtBegin--;
        }
    }
}
