using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlace : MonoBehaviour
{
    public SpawnItem[] SpawningVariants;

    public void Spawn()
    {
        Debug.Log("Try to Spawn");
        foreach (var item in SpawningVariants)
        {
            if (CheckProbabilty(item.Count_Spawn_Probability()))
            {
                GameObject newobj = item.Spawn();
                newobj.transform.position = transform.position;
                break;
            }
        }
    }

    private bool CheckProbabilty(float p)
    {
        double r = Random.Range(0.0f,1.0f);
        return r > p;
    }
}
