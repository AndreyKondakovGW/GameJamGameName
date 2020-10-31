using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreceRegenerationEfect : Efect
{
    public float delta;

    public override void Use()
    {
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().Regeneration += delta;
    }
}
