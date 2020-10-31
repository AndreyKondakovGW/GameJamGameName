using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightEyeEfect : Efect
{
    public override void Use()
    {
        GameObject.FindGameObjectsWithTag("Player")[0].transform.Find("DirLight").gameObject.GetComponent<Light>().intensity = 0.3f;
    }
}
