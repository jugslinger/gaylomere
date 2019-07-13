using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    int coins = 0;
    Vector3 startingPosition;
    private float Speed = 5f;
    private Vector3 input;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the rigidbody component added to the object and store it in rb
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //rb.AddForce(new Vector3(-100, 0, 0));
            transform.position += new Vector3(-1, 0, 0) * Speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //rb.AddForce(new Vector3(100, 0, 0));
            transform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 100, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D col) // col is the trigger object we collided with
    {
        if (col.tag == "Coin")
        {
            coins++;
            Destroy(col.gameObject); // remove the coin
        }
        else if (col.tag == "Water")
        {
            // Death? Reload Scene? Teleport to start:
            transform.position = startingPosition;
        }
        else if (col.tag == "Spike")
        {
            // Death? Reload Scene? Teleport to start:
            transform.position = startingPosition;
        }
        else if (col.tag == "End")
        {
            // Load next level? Heres how you get this level's scene number, add 1 to it and load that scene:
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
