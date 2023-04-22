using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultControls : MonoBehaviour
{
    void Start()
    {
        //if controls have not been set, then make them the default
        if(!PlayerPrefs.HasKey("Control1"))
        {
            PlayerPrefs.SetInt("Control1", (int)KeyCode.D);
        }

        if(!PlayerPrefs.HasKey("Control2"))
        {
            PlayerPrefs.SetInt("Control2", (int)KeyCode.F);
        }

        if(!PlayerPrefs.HasKey("Control3"))
        {
            PlayerPrefs.SetInt("Control3", (int)KeyCode.J);
        }

        if(!PlayerPrefs.HasKey("Control4"))
        {
            PlayerPrefs.SetInt("Control4", (int)KeyCode.K);
        }

        //initial volume slider value is 1
        if(!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
        }

        PlayerPrefs.SetInt("Health", 6);
        PlayerPrefs.SetInt("PauseActive", 0);
        PlayerPrefs.SetInt("CountdownActive", 0);
        PlayerPrefs.SetInt("Boss_fight_introduction_complete", 0);
    }
}
