using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlace : MonoBehaviour
{
    public SpawnItem[] SpawningVariants;

    public void Spawn()
    {
        foreach (var item in SpawningVariants)
        {
            if (CheckProbabilty(item.Count_Spawn_Probability()))
            {
                GameObject newobj = item.Spawn();
                
                Vector3 pos = this.transform.position;
                newobj.transform.position = pos;
                newobj.transform.SetParent(this.transform);
                break;
            }
        }
    }

    private bool CheckProbabilty(float p)
    {
        double r = Random.Range(0.0f,1.0f);
        return r < p;
    }
}
