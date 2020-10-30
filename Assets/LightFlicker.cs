using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light light;

    public float maxRange;
    public float minRange;

    void Update()
    {
        float newRang = Random.Range(minRange, maxRange);
        light.range = newRang;
    }
}
