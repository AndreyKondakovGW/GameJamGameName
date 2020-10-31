using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float HP;
    public float maxHP;

    public float Srength= 0f;
    public float Endurance = 0f;
    public float Agility = 0f;
    public float Damage = 10f;
    public float Regeneration = 0f;

    public UIHealthBar HB;
    void Start()
    {
        StartCoroutine(CountRegen());
    }

    IEnumerator CountRegen()
    {
        while (true)
        {
            if (HP > maxHP)
            {
                HP = maxHP;
            }
            HP += Regeneration;
            if (HP > maxHP)
            {
                HP = maxHP;
            }
            yield return new WaitForSeconds(2);
        }

    }

    // Update is called once per frame
    void Update()
    {
        HB.UpdateHP(HP);
        HB.UpdateMaxHP(maxHP, HP);
        
    }
}
