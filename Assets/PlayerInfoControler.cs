using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoControler : MonoBehaviour
{
    public int curr_Inventory_page = 1;
    public int page_size = 3;

    private Inventory Inventory;

    public ItemInfoControler[] InventoryСontent;

    public GameObject InvrntoryPanel;
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
                is_active = false;
            }
            else
            {
                InvrntoryPanel.SetActive(true);
                is_active = true;
                InitAllData();
            }
        }
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
