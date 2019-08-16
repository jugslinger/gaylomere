﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float pauseTimer;
    Vector3 moveDistance;

    void Update()
    {
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
    }
}
