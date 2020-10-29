using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.tag == "Player")
        {
            Debug.Log("Переходим на следующий уровень");
            GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameControler>().ChangeLevel();
        }
        
    }
}
