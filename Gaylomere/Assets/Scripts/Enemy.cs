using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float pauseTimer;
    Vector3 moveDistance;

    private GameObject[] targets;
    private GameObject closest;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        float distance = Mathf.Infinity;

        do
        {
            moveDistance.x = Random.Range(-6, 7);
        } while (moveDistance.x == 0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDistance, Time.deltaTime);

        pauseTimer = Random.Range(1000, 6000);
        do
        {
            pauseTimer -= Time.deltaTime;
        } while (pauseTimer > 0);

        foreach (GameObject target in targets)
        {
            Vector3 diff = target.transform.position - transform.position;
            if (diff.sqrMagnitude < distance)
            {
                closest = target;
                distance = diff.sqrMagnitude;
            }
        }
        if (distance <= 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, Time.deltaTime);
            if(closest.transform.position.y > transform.position.y)
            {
                rb.AddForce(new Vector3(0, 200, 0));
            }
        }
    }
}
