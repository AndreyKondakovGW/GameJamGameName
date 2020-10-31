using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    public int curent_level = 1;
    public int last_level = 100;
    public float speedfinemodifiee = 0.9f;
    public GameObject RoomControler;
    public UIControler UIController;
    public GameObject Player;
    

    public Artifact[] GlobalArtifactList; 
    

    //efect
    public bool Clairvoyance = false;
    private void ShowIntroduction()
    {
        UIController.ShowMesage("Я отправился в путешествие, чтобы одолеть древнее зло, что терзает эти земли. \n Но я слаб и беспомощен, а мои враги могучи. Мне нужно набраться сил. Найти больше Артефактов.\n", 10);
    }
    // Start is called before the first frame update
    void Start()
    {   
       Time.timeScale = 1f;
       RoomControler = GameObject.FindGameObjectsWithTag("RoomControler")[0];
       UIController = GameObject.FindGameObjectsWithTag("UIController")[0].GetComponent<UIControler>();
       Player = GameObject.FindGameObjectsWithTag("Player")[0];

       ShowIntroduction();
    }

    public void ChangeLevel()
    {
        curent_level++;
        Player.transform.position = GameObject.FindGameObjectsWithTag("Respawn")[0].transform.position;
        foreach (var r in GameObject.FindGameObjectsWithTag("Room"))
        {
            Destroy(r);
        }

        if (curent_level == last_level)
        {
            RoomControler.GetComponent<RoomSectionControler>().CreatelastLevel();
        }
        else
        {
            RoomControler.GetComponent<RoomSectionControler>().CreateSection();
        }
        
        UIController.ShowMesage("Вы перешли на уровень " + curent_level);
        if (Clairvoyance)
        {
            foreach (var en in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                en.transform.Find("Area Light").gameObject.GetComponent<Light>().intensity = 1;
            }
        }

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
        Player.GetComponent<PlayerController>().movementSpeed = Player.GetComponent<PlayerController>().movementSpeed * speedfinemodifiee;
        UIController.ShowMesage("Сокровища отягощают вас!");
    }

    public IEnumerator EndGame()
    {
        UIController.ShowMesage("Вы Умерли", 5);
        Time.timeScale = 0f;
        yield return new  WaitForSecondsRealtime(5);
        SceneManager.LoadScene("MainMenu");
        
    }
}
