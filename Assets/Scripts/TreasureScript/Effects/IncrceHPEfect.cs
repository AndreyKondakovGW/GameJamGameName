﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrceHPEfect : Efect
{
    public float delta;
    public bool is_procentag;

    public override void Use()
    {
        if (is_procentag)
        {
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().maxHP *= (1 + delta / 100);
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().HP *= (1 + delta / 100);
        }
        else
        {
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().maxHP += delta;
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().HP += delta;
        }
    }
}
