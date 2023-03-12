using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForPauseScreen : MonoBehaviour
{
    public GameObject panel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (PlayerPrefs.GetInt("PauseActive") == 0) {
                panel.SetActive(true);
                PlayerPrefs.SetInt("PauseActive", 1);
            }
            else if (PlayerPrefs.GetInt("PauseActive") == 1) {
                panel.SetActive(false);
                PlayerPrefs.SetInt("PauseActive", 0);
            }
        }
    }
}
