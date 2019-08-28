using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float pauseTimer;
    Vector3 lastLocation;
    Vector3 moveDistance;

    private GameObject[] targets;
    private GameObject closest;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targets = GameObject.FindGameObjectsWithTag("Player");
        do
        {
            moveDistance.x = Random.Range(-6, 7);
        } while (moveDistance.x == 0);
    }

    void Update()
    {
        float distance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            Vector3 diff = target.transform.position - transform.position;
            if (diff.sqrMagnitude < distance)
            {
                closest = target;
                distance = diff.sqrMagnitude;
            }
        }
        if (distance <= 20)
        {
            transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, Time.deltaTime * 4.0f);
            if (closest.transform.position.y > transform.position.y + 1)
            {
                rb.AddForce(new Vector3(0, 20, 0));
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDistance, Time.deltaTime);

            if (Vector3.Distance(transform.position, transform.position + moveDistance) < 0.2f || (transform.position.x >= lastLocation.x - 0.02 && transform.position.x <= lastLocation.x + 0.02)) //lastLocation needs to be adjusted to stop ai from sticking to walls
            {
                if (pauseTimer <= 0 || (transform.position.x >= lastLocation.x - 0.02 && transform.position.x <= lastLocation.x + 0.02))
                {
                    do
                    {
                        moveDistance.x = Random.Range(-6, 7);
                    } while (moveDistance.x == 0);
                    pauseTimer = Random.Range(1, 6);
                }
                else
                {
                    pauseTimer -= Time.deltaTime;
                }
            }
        }
        lastLocation = transform.position;
        Debug.Log("x: " + lastLocation.x.ToString());
    }
}
