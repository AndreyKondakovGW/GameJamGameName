using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControler : MonoBehaviour
{
    public delegate void Action();
    public TextLogPanelControler TLC;
    public ChoicePanelControler CPC;

    public void ShowMesage(string m, float d = 2f)  
    {
        StartCoroutine(TLC.ShowMessage(m,d));
    }

    public void ShowDialogMessage(string t,string m,Action action)
    {
        StartCoroutine(CPC.ShowPanel(t,m,action));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
