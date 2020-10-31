using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class SpawnItem : MonoBehaviour
{
    public int level = 1;
    public float max_Probability = 0.75f;
    public float defalt_Probability = 0.5f;

    public GameObject Object;

    public float Count_Spawn_Probability()
    {
        int global_lvl = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameControler>().curent_level;
        return Math.Min((defalt_Probability - defalt_Probability * (Math.Abs(global_lvl - level) / (10f))), max_Probability);    
    }

    public GameObject Spawn()
    {
        return Instantiate(Object);
    }
}
