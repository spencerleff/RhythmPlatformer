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
        yield return new WaitForSeconds(2.25f);
        countdownText.text = "GO!";
        
        yield return new WaitForSeconds(0.75f);
        countdownText.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.7f);
        music.Play();
        PlayerPrefs.SetInt("CountdownActive", 0);
    }
}