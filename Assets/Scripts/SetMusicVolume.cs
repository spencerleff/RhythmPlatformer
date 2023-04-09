using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicVolume : MonoBehaviour
{
    public AudioSource music;

    void Start()
    {   
        music.volume = PlayerPrefs.GetFloat("Volume");
    }
}