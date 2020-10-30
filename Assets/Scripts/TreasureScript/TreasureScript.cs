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
                UIController.ShowDialogMessage("Вы нашли Сокровище", "Это сокровище совсем бесполезно", PickUp);
            }
            else
            {
               GameObject.FindGameObjectsWithTag("UIController")[0].GetComponent<UIControler>().ShowMesage("Вы ничего не нашли"); 
            }
            
        }
        
    }
}
