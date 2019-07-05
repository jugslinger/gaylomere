using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float speed = 3f;
    Rigidbody2D rb;
    int coins = 0;
    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the rigidbody component added to the object and store it in rb
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var input = Input.GetAxis("Horizontal"); // This will give us left and right movement (from -1 to 1). 
        var movement = input * speed;

        rb.velocity = new Vector3(movement, rb.velocity.y, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 100, 0)); // Adds 100 force straight up, might need tweaking on that number
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
