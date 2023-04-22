using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fight_Info : MonoBehaviour
{
    public Sprite heart_full;
    public Sprite heart_half;
    public Sprite heart_empty;
    public Image heart1;
    public Image heart2;
    public Image heart3;

    private int health;

    void Start()
    {
        health = PlayerPrefs.GetInt("Health");
        SetHP(health);
    }

    private void SetHP(int hp) {
        if (hp == 1) {
            heart1.sprite = heart_half;
            heart2.sprite = heart_empty;
            heart3.sprite = heart_empty;
        }
        else if (hp == 2) {
            heart1.sprite = heart_full;
            heart2.sprite = heart_empty;
            heart3.sprite = heart_empty;
        }
        else if (hp == 3) {
            heart1.sprite = heart_full;
            heart2.sprite = heart_half;
            heart3.sprite = heart_empty;
        }
        else if (hp == 4) {
            heart1.sprite = heart_full;
            heart2.sprite = heart_full;
            heart3.sprite = heart_empty;
        }
        else if (hp == 5) {
            heart1.sprite = heart_full;
            heart2.sprite = heart_full;
            heart3.sprite = heart_half;
        }
        else {
            heart1.sprite = heart_full;
            heart2.sprite = heart_full;
            heart3.sprite = heart_full;
        }
    }
}
