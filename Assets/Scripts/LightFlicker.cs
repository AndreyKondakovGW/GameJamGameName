using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    //public Light light;

    public float defalt;
    public float delta;
    //public float maxRange;
    //public float minRange;

    void Update()
    {
        float newRang = Random.Range(defalt - delta, defalt + delta);
        gameObject.GetComponent<Light>().range = newRang;
    }
}
