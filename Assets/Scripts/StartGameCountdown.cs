using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGameCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public AudioSource music;

    void Start()
    {
        PlayerPrefs.SetInt("CountdownActive", 1);
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        //extreme countdown (1.2x faster)
        if (PlayerPrefs.GetInt("Extreme_Active") == 1) {
            yield return new WaitForSeconds((2.25f / 1.2f));
            countdownText.text = "GO!";
            
            yield return new WaitForSeconds((0.75f / 1.2f));
            countdownText.gameObject.SetActive(false);

            yield return new WaitForSeconds((0.7f / 1.2f));
            music.pitch = 1.2f;
            music.Play();
            PlayerPrefs.SetInt("CountdownActive", 0);
        }

        //normal countdown
        else {
            yield return new WaitForSeconds(2.25f);
            countdownText.text = "GO!";
        
            yield return new WaitForSeconds(0.75f);
            countdownText.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.7f);
            music.Play();
            PlayerPrefs.SetInt("CountdownActive", 0);
        }
    }
}