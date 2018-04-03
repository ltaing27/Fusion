﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour {

    public GameObject zombie;
    public int maxAliveAtOnce = 10;
    List<GameObject> zombiesList;

    int numGeneratedTotal = 0; //number of zombies generated

    int numToGenerate = 0;//# of zombies to generate this round
    int numGeneratedRecent = 0;//num generated since last call to "GenerateNewZombies"

    int score = 0;
    int scoreIncrease = 100;

    // Use this for initialization
    void Start(){
    }

    public void GenerateNewZombies(int amount) //resets list and generates a new group
    {
        numGeneratedRecent = 0;
        this.numToGenerate = amount;
        zombiesList = new List<GameObject>();
        for (int i = 0; i < maxAliveAtOnce && i < amount; i++)
        {
            spawnZombie();
        }
    }

    private void spawnZombie(){ // just initialized; not active just yet
        GameObject obj = (GameObject)Instantiate(zombie);
        obj.SetActive(false);
        zombiesList.Add(obj);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleZombiePooling();
    }

    private void HandleZombiePooling()
    {
        //Debug.Log(maxAliveAtOnce + " " + numToGenerate + " " + numGeneratedRecent);
        for (int i = 0; i < maxAliveAtOnce && i < numToGenerate && numGeneratedRecent < numToGenerate; i++)
        {
            if (!zombiesList[i].activeInHierarchy)
            {
                numGeneratedRecent++;
                numGeneratedTotal++;
                zombiesList[i].transform.position = new Vector3(Random.Range(-20, 20), zombiesList[i].transform.localScale.y, Random.Range(-10, 10));
                zombiesList[i].GetComponent<ZombieController>().reset();
                zombiesList[i].SetActive(true);
                break;
            }
        }

        for (int i = 0; i < maxAliveAtOnce && i < numToGenerate; i++)
        {
            if (zombiesList[i].GetComponent<ZombieController>().getHealth() <= 0) // remove platform after player has gone 50 units past it
            {
                zombiesList[i].SetActive(false);
                
                //currently, keeps track of score but doesn't show up until first collision
                var scoreText = GameObject.Find("Score_Text").GetComponent<UnityEngine.UI.Text>();
                scoreText.color = new Color(1, 0, 0);
                score += scoreIncrease;
                scoreText.text = "Score: " + score;
                
            }
        }
    }

   

    private void RemovePlatforms()
    {
        foreach (GameObject obj in zombiesList)
        {
            Destroy(obj);
        }
    }

    public void Reset()
    {
        numGeneratedTotal = 0;
        numGeneratedRecent = 0;
        numToGenerate = 0;
        zombiesList = null;
    }
}
