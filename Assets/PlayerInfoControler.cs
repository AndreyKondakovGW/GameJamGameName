using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoControler : MonoBehaviour
{
    public Text HP;
    public Text Srength;
    public Text Endurance;
    public Text Agility;
    public Text Damage;

    public int curr_Inventory_page = 1;
    public int page_size = 3;

    private Inventory Inventory;

    public ItemInfoControler[] InventoryСontent;

    public GameObject InvrntoryPanel;

    public GameObject StatPanel;
    private bool is_active = false;

    void Start()
    {
        Inventory = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (is_active)
            {
                InvrntoryPanel.SetActive(false);
                StatPanel.SetActive(false);
                is_active = false;
            }
            else
            {
                InvrntoryPanel.SetActive(true);
                StatPanel.SetActive(false);
                is_active = true;
                InitAllData();
            }
        }
    }

    public void ShowStats()
    {
        InvrntoryPanel.SetActive(false);
        StatPanel.SetActive(true);
    }
    public void ShowInventar()
    {
        InvrntoryPanel.SetActive(true);
        StatPanel.SetActive(false);
    }

    public void InitAllData()
    {
           Artifact[] Data = Inventory.ShowArtList(curr_Inventory_page, page_size);

            for (int i = 0; i< page_size;i++)
            {
                if (i >= Data.Length)
                {
                    InventoryСontent[i].gameObject.SetActive(false);
                }else
                {
                    InventoryСontent[i].gameObject.SetActive(true);
                    InventoryСontent[i].InitInfo(Data[i].Name, Data[i].Description,Data[i].Image);
                }
                
            }

            PlayerStats ps = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>();
            HP.text = "HP :" + ps.maxHP.ToString();
            Srength.text ="Сила :" + ps.Srength.ToString();
            Endurance.text ="Выносливость :" + ps.Endurance.ToString();
            Agility.text ="Ловкость :" + ps.Agility.ToString();
            Damage.text ="Урон :" + ps.Damage.ToString();
       
    }

    public void ChangePageRight()
    {
        int page_count = ((Inventory.InventorySize + page_size - 1) / page_size);
        if (page_count > 1)
        {
           curr_Inventory_page = (curr_Inventory_page + 1) % page_count;
           if (curr_Inventory_page == 0){ curr_Inventory_page = page_count;}
           InitAllData();
        }
        Debug.Log(curr_Inventory_page);

    }

    public void ChangePageLeft()
    {
        int page_count = ((Inventory.InventorySize + page_size - 1) / page_size);
        if (page_count > 1)
        {
           curr_Inventory_page = (page_count + curr_Inventory_page - 1) % page_count;
           if (curr_Inventory_page == 0){ curr_Inventory_page = page_count;}
           InitAllData();
        }
        Debug.Log(curr_Inventory_page);
    }


}
