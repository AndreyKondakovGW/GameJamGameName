using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public int curent_level = 1;
    public float speedfinemodifiee = 0.9f;
    public GameObject RoomControler;
    public UIControler UIController;
    public GameObject Player;

    public Artifact[] GlobalArtifactList; 
    

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
        Player.transform.position = GameObject.FindGameObjectsWithTag("Respawn")[0].transform.position;
        foreach (var r in GameObject.FindGameObjectsWithTag("Room"))
        {
            Destroy(r);
        }
        RoomControler.GetComponent<RoomSectionControler>().CreateSection();
        UIController.ShowMesage("Вы перешли на уровень" + curent_level);
    }

    public Artifact GrnerateArtifact()
    {
        int length = GlobalArtifactList.Length;

        while (length-- >0)
        {
            var a = GlobalArtifactList[Random.Range(0, GlobalArtifactList.Length)];
            float p = a.Count_Spawn_Probability();
            double r = Random.Range(0.0f,1.0f);
            if (r < p)
            {
                return a;
            }
        }
        return null;
    }

    public void DecrecePalyerSpeed()
    {
        Player.GetComponent<CharacterTestScript>().moveSpeed = Player.GetComponent<CharacterTestScript>().moveSpeed * speedfinemodifiee;
        UIController.ShowMesage("Сокровища отягощают вас!");
    }
}
