﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreceAgilityEfect : Efect
{
    public float delta;
    public bool is_procentag;

    public override void Use()
    {
        if (is_procentag)
        {
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().Agility *= (1 + delta / 100);
        }
        else
        {
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().Agility += delta; 
        }
        
    }
}
