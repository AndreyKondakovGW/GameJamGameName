using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelControler : MonoBehaviour
{
    public Text messageTiltel;
    public Text messageFiled;
    public UIControler.Action my_Action;

    public GameObject ChoicePanel;

    public IEnumerator ShowPanel(string tiltel, string message, UIControler.Action action)
    {
        ChoicePanel.SetActive(true);

        messageTiltel.text = tiltel;
        messageFiled.text = message;
        my_Action = action;

        yield return new WaitForSeconds(10f);
        ChoicePanel.SetActive(false);
    }

    public void YesAction()
    {
        my_Action();
        ChoicePanel.SetActive(false);
    }

    public void NoAction()
    {
        ChoicePanel.SetActive(false);
    }
}
