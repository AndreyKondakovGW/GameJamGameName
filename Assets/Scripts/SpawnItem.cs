using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class SpawnItem : MonoBehaviour
{
    public int level = 1;
    public double max_Probability = 0.75;

    public GameObject Object;

    public double Count_Spawn_Probability()
    {
        int global_lvl = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameControler>().curent_level;
        return Math.Min((0.75 - Math.Abs(global_lvl - level) * 0.75), max_Probability);    
    }

    public GameObject Spawn()
    {
        return Instantiate(Object);
    }
}
