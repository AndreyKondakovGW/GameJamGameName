using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject Door_U;
    public GameObject Door_B;
    public GameObject Door_R;
    public GameObject Door_L;

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
        Door_U.SetActive(true);
        Door_B.SetActive(true);
        Door_R.SetActive(true);
        Door_L.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
