using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateRebindText : MonoBehaviour
{
    public TextMeshProUGUI CurrentC1;
    public TextMeshProUGUI CurrentC2;
    public TextMeshProUGUI CurrentC3;
    public TextMeshProUGUI CurrentC4;

    private KeyCode control1;
    private KeyCode control2;
    private KeyCode control3;
    private KeyCode control4;
    
    public void update_rebind_text()
    {
        control1 = (KeyCode)PlayerPrefs.GetInt("Control1");
        control2 = (KeyCode)PlayerPrefs.GetInt("Control2");
        control3 = (KeyCode)PlayerPrefs.GetInt("Control3");
        control4 = (KeyCode)PlayerPrefs.GetInt("Control4");

        CurrentC1.text = control1.ToString();
        CurrentC2.text = control2.ToString();
        CurrentC3.text = control3.ToString();
        CurrentC4.text = control4.ToString();
    }
}
