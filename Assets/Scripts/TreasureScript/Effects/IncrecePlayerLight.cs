using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrecePlayerLight : Efect
{
    public float delta;

    public override void Use()
    {
        float d = GameObject.FindGameObjectsWithTag("PlayerLight")[0].GetComponent<LightFlicker>().defalt;
        d = d * 1+(delta / 100);
    }
}
