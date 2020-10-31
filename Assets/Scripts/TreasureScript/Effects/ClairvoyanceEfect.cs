using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClairvoyanceEfect : Efect
{
    public override void Use()
    {
        foreach (var en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            en.transform.Find("Area Light").gameObject.GetComponent<Light>().intensity = 1;
        }

        GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameControler>().Clairvoyance = true;
    }
}
