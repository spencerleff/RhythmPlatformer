using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioSource music;

    void Start()
    {
        music.volume = PlayerPrefs.GetFloat("Volume");
        music.Play();
    }
}
