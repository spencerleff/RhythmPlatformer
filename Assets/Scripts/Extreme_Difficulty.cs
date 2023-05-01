using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Extreme_Difficulty : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image frog;
    public RawImage background;
    public AudioSource music;

    void Start() {
        if (PlayerPrefs.GetInt("Extreme_Active") == 1) {
            text.gameObject.SetActive(true);
            frog.color = new Color(1f, 0f, 0f, 1f);
            music.pitch = 1.2f;
            background.color = new Color(1f, 0.3f, 0.3f, 1f);
        }
        else {
            text.gameObject.SetActive(false);
            frog.color = new Color(1f, 1f, 1f, 1f);
            music.pitch = 1f;
            background.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void Update() {
        //text animation
        if (PlayerPrefs.GetInt("Extreme_Active") == 1) {
            Vector3 scale = text.transform.localScale;
            
            float min = 0.9f;
            float max = 1.1f;

            float newScale = Mathf.Lerp(min, max, Mathf.PingPong(Time.time, 1));
            text.transform.localScale = new Vector3(newScale, newScale, scale.z);
        }
    }

    public void Toggle_Extreme_Difficulty() {
        if (PlayerPrefs.GetInt("Extreme_Active") == 0) {
            PlayerPrefs.SetInt("Extreme_Active", 1);
            StartCoroutine(fadeInItems());
        }
        else {
            PlayerPrefs.SetInt("Extreme_Active", 0);
            StartCoroutine(fadeOutItems());
        }

    }

    public IEnumerator fadeInItems() {
        Color startColor = text.color;
        startColor.a = 0f;
        Color endColor = text.color;
        endColor.a = 1f;
        text.color = startColor;
        text.gameObject.SetActive(true);

        Color frog_startColor = frog.color;
        Color frog_endColor = new Color(1f, 0f, 0f, 1f);

        Color bg_startColor = background.color;
        Color bg_endColor = new Color(1f, 0.3f, 0.3f, 1f);

        float t = 0f;
        float fadeDuration = 0.5f;

        while (t < fadeDuration) {
            t += Time.deltaTime;
            text.color = Color.Lerp(startColor, endColor, t / fadeDuration);
            frog.color = Color.Lerp(frog_startColor, frog_endColor, t / fadeDuration);
            background.color = Color.Lerp(bg_startColor, bg_endColor, t / fadeDuration);
            music.pitch = Mathf.Lerp(1f, 1.2f, t / fadeDuration);
            
            yield return null;
        }
        
        text.color = endColor;
        frog.color = frog_endColor;
    }

    public IEnumerator fadeOutItems() {
        Color startColor = text.color;
        startColor.a = 1f;
        Color endColor = text.color;
        endColor.a = 0f;
        text.color = startColor;

        Color frog_startColor = frog.color;
        Color frog_endColor = new Color(1f, 1f, 1f, 1f);

        Color bg_startColor = new Color(1f, 0.3f, 0.3f, 1f);
        Color bg_endColor = new Color(1f, 1f, 1f, 1f);

        float t = 0f;
        float fadeDuration = 0.5f;

        while (t < fadeDuration) {
            t += Time.deltaTime;
            text.color = Color.Lerp(startColor, endColor, t / fadeDuration);
            frog.color = Color.Lerp(frog_startColor, frog_endColor, t / fadeDuration);
            background.color = Color.Lerp(bg_startColor, bg_endColor, t / fadeDuration);
            music.pitch = Mathf.Lerp(1.2f, 1f, t / fadeDuration);
            yield return null;
        }

        text.color = endColor;
        frog.color = frog_endColor;

        text.gameObject.SetActive(false);
    }
}
