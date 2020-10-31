using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoControler : MonoBehaviour
{
    public Image icon;

    public Text Tiltel;
    public Text Information;

    public void InitInfo(string t, string i, Sprite s)
    {
        Tiltel.text = t;
        Information.text = i;

        icon.sprite = s;
    }
}
