using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    void Action1 ()
    {
        GameObject.FindGameObjectsWithTag("UIController")[0].GetComponent<UIControler>().ShowMesage("Вы подобрали сокровище");
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Вы Нашли Сокровище");
            UIControler UIController = GameObject.FindGameObjectsWithTag("UIController")[0].GetComponent<UIControler>();
            UIController.ShowDialogMessage("Вы нашли Сокровище", "Это сокровище совсем бесполезно", Action1);
        }
        
    }
}
