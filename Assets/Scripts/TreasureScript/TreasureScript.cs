using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    public Artifact Art;
    public bool is_played = false;
    void Start()
    {
        Art = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameControler>().GrnerateArtifact();
    }

    void PickUp()
    {
        var inventary = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Inventory>();
        inventary.AddArtifact(Art);
        foreach (var ef in Art.efect)
        {
            ef.Use();
        }
        //GameObject.FindGameObjectsWithTag("UIController")[0].GetComponent<UIControler>().ShowMesage("Вы подобрали сокровище");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "Player") && !(is_played))
        {
            is_played = true;
            if (Art)
            {
                UIControler UIController = GameObject.FindGameObjectsWithTag("UIController")[0].GetComponent<UIControler>();
                UIController.ShowDialogMessage(Art.Name, Art.Description + "\n Хотите Подобрать?", PickUp);
            }
            else
            {
               GameObject.FindGameObjectsWithTag("UIController")[0].GetComponent<UIControler>().ShowMesage("Вы ничего не нашли"); 
            }
            
        }
        
    }
}
