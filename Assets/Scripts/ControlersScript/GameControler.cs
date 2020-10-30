﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public int curent_level = 1;
    public GameObject RoomControler;
    public UIControler UIController;
    public GameObject Player;
    

    // Start is called before the first frame update
    void Start()
    {   
       RoomControler = GameObject.FindGameObjectsWithTag("RoomControler")[0];
       UIController = GameObject.FindGameObjectsWithTag("UIController")[0].GetComponent<UIControler>();
       Player = GameObject.FindGameObjectsWithTag("Player")[0];

       UIController.ShowMesage("Игра началась!");
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
        UIController.ShowMesage("Вы перешли на уровень" + curent_level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}