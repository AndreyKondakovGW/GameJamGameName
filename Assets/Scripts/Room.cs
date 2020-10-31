using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float Probability = 0.5f;

    public GameObject Door_U;
    public GameObject Door_B;
    public GameObject Door_R;
    public GameObject Door_L;

    public SpawnPlace[] Spawns;

    public bool rotatable = true;

    public void activateRoom()
    {
        foreach (var s in Spawns)
        {
            s.Spawn();
        }
    }

    public void RotateRandomly()
    {
        int c = Random.Range(0,4);

        for (var i = 0; i < c; i++)
        {
            transform.Rotate(0,0,-90);

            GameObject t = Door_R;
            Door_R = Door_U;
            Door_U = Door_L;
            Door_L = Door_B;
            Door_B = t;
        }
    }

    public void LockAllDoors()
    {
        if (Door_U)
        {
            Door_U.SetActive(true);
        }
        if (Door_B)
        {
            Door_B.SetActive(true);
        }
        if (Door_R)
        {
            Door_R.SetActive(true);
        }
        if (Door_L)
        {
            Door_L.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckProbability()
    {
        float f = Random.Range(0f,1f);
        return f < Probability;
    }
}
