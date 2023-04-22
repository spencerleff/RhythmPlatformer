using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicVolume : MonoBehaviour
{
    public AudioSource music;
    public AudioSource gameOverSound;
    public AudioSource healSound;
    public AudioSource hurtSound;

    void Start()
    {   
        music.volume = PlayerPrefs.GetFloat("Volume");
        gameOverSound.volume = PlayerPrefs.GetFloat("Volume");
        healSound.volume = PlayerPrefs.GetFloat("Volume");
        hurtSound.volume = PlayerPrefs.GetFloat("Volume");
    }
}