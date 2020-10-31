using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenueControler : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject SettingsPanel;
    public GameObject QuitPanel;
    public string StartGameSceneName = "SampleScene";

    public AudioMixerGroup Mixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuitPanel()
    {
        MainPanel.SetActive(false);
        QuitPanel.SetActive(true);
    }

    public void ShowMainPanel()
    {
        MainPanel.SetActive(true);
        QuitPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        MainPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("Volume", Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeMuiscVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MuiscVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(StartGameSceneName);
    }

}
