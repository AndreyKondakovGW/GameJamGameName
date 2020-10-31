using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlace : MonoBehaviour
{
    public SpawnItem[] SpawningVariants;

    public void Spawn()
    {
        int length = SpawningVariants.Length;
        while (length -- > 0)
        {
            var item = SpawningVariants[Random.Range(0, SpawningVariants.Length)];
            GameObject newobj = item.Spawn();
                
            Vector3 pos = this.transform.position;
            newobj.transform.position = pos;
            newobj.transform.SetParent(this.transform);
            break;
        }
    }

    private bool CheckProbabilty(float p)
    {
        double r = Random.Range(0.0f,1.0f);
        return r < p;
    }
}
