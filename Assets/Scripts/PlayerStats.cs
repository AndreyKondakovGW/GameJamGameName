using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int HP;
    public int maxHP;

    public UIHealthBar HB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HB.UpdateHP(HP);
        HB.UpdateMaxHP(maxHP, HP);
    }
}
