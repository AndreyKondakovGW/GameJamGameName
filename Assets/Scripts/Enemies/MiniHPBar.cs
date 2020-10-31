using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHPBar : MonoBehaviour
{
    Transform fullBar;
    Transform Pivot;

    private void Start()
    {
        if (!fullBar)
        {
            Pivot = transform.Find("Pivot");
            fullBar = Pivot.Find("full");
        }
        UpdateHPByRatio(1);
    }
    public void UpdateHPByRatio(float hp)
    {
        if (hp == 1)
        {
            Pivot.gameObject.SetActive(false);
        }
        else if (hp <= 0)
        {
            Pivot.gameObject.SetActive(false);
        }
        else
        {
            fullBar.localScale = new Vector3(hp, fullBar.localScale.y);
            Pivot.gameObject.SetActive(true);
        }
    }
}
