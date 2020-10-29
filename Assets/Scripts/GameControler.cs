using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public int curent_level = 1;
    public GameObject RoomControler;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {   
       RoomControler = GameObject.FindGameObjectsWithTag("RoomControler")[0];
       Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    public void ChangeLevel()
    {
        curent_level++;
        Player.transform.position = new Vector3(0,0,0);
        foreach (var r in GameObject.FindGameObjectsWithTag("Room"))
        {
            Destroy(r);
        }
        RoomControler.GetComponent<RoomSectionControler>().CreateSection();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
