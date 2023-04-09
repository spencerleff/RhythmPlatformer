using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForPauseScreen : MonoBehaviour
{
    public GameObject panel;
    public AudioSource music;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerPrefs.GetInt("CountdownActive") == 0) {
            if (PlayerPrefs.GetInt("PauseActive") == 0) {
                music.Pause();
                panel.SetActive(true);
                panel.transform.SetAsLastSibling();
                PlayerPrefs.SetInt("PauseActive", 1);
            }
            else if (PlayerPrefs.GetInt("PauseActive") == 1) {
                music.UnPause();
                panel.SetActive(false);
                PlayerPrefs.SetInt("PauseActive", 0);
            }
        }
    }

    public void PressedResumeButton()
    {
        music.UnPause();
        panel.SetActive(false);
        PlayerPrefs.SetInt("PauseActive", 0);
    }
}
