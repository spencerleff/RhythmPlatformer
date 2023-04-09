using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVolume : MonoBehaviour
{
    public Slider slider;

    public void update_volume()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
    }

    public void update_volume_slider()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
    }
}
