using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLogPanelControler : MonoBehaviour
{
    public Text messageFiled;
    public GameObject messagPanel;
    
    public IEnumerator ShowMessage(string new_message, float duration)
    {
        messagPanel.SetActive(true);
        messageFiled.text = new_message;
        yield return new WaitForSeconds(duration);
        messagPanel.SetActive(false);
    }

}
