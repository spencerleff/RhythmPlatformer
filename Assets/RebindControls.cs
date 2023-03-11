using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RebindControls : MonoBehaviour
{
    public TMP_InputField C1;
    public TMP_InputField C2;
    public TMP_InputField C3;
    public TMP_InputField C4;

    public TextMeshProUGUI CurrentC1;
    public TextMeshProUGUI CurrentC2;
    public TextMeshProUGUI CurrentC3;
    public TextMeshProUGUI CurrentC4;

    public void SaveControls() {
        KeyCode C1KeyCode;
        if (Enum.TryParse(C1.text.ToUpper(), out C1KeyCode)) {
            PlayerPrefs.SetInt("Control1", (int)C1KeyCode);
            CurrentC1.text = C1.text.ToUpper();
        }

        KeyCode C2KeyCode;
        if (Enum.TryParse(C2.text.ToUpper(), out C2KeyCode)) {
            PlayerPrefs.SetInt("Control2", (int)C2KeyCode);
            CurrentC2.text = C2.text.ToUpper();
        }

        KeyCode C3KeyCode;
        if (Enum.TryParse(C3.text.ToUpper(), out C3KeyCode)) {
            PlayerPrefs.SetInt("Control3", (int)C3KeyCode);
            CurrentC3.text = C3.text.ToUpper();
        }

        KeyCode C4KeyCode;
        if (Enum.TryParse(C4.text.ToUpper(), out C4KeyCode)) {
            PlayerPrefs.SetInt("Control4", (int)C4KeyCode);
            CurrentC4.text = C4.text.ToUpper();
        }

        C1.text = "";
        C2.text = "";
        C3.text = "";
        C4.text = "";
    }
}
