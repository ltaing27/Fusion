﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    public int startingHealth;
    public float speed = 1;
    int health = 100;

    public int getHealth(){
        return health;
    }

    public void resetHealth()
    {
        health = startingHealth;
    }

	// Use this for initialization
	void Start () {
        health = startingHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        followPlayer();
	}

    private void followPlayer(){
        Vector3 playerPos = GameObject.Find("Player").transform.position;

        float magnitudeDistance = (playerPos - transform.position).magnitude;
        float newX = (playerPos.x - transform.position.x)/magnitudeDistance * speed * Time.deltaTime;
        float newZ = (playerPos.z - transform.position.z) / magnitudeDistance * speed * Time.deltaTime;;

        transform.position = new Vector3(newX, transform.localScale.y, newZ);
    }
}