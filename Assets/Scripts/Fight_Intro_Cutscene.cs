using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fight_Intro_Cutscene : MonoBehaviour
{
    public Image character;
    public Image character_with_bag;
    public Image bag;
    public Image witch;
    public GameObject dialogue_panel;
    public TextMeshProUGUI char_name;
    public TextMeshProUGUI textbox;

    public GameObject controls_panel;
    public TextMeshProUGUI objective_text;
    public TextMeshProUGUI left_control_title;
    public TextMeshProUGUI left_control_text;
    public TextMeshProUGUI right_control_title;
    public TextMeshProUGUI right_control_text;

    public AudioSource intro_music;

    //dialogue
    private string[] names;
    private string[] sentences;
    private int counter = 0;
    private int total = 1;
    private float textSpeed = 0.03f;

    void Start()
    {
        PlayerPrefs.SetInt("Boss_fight_introduction_complete", 0);
        PlayerPrefs.SetInt("CountdownActive", 1);
        intro_music.volume = PlayerPrefs.GetFloat("Volume");
        intro_music.Play();
        StartCoroutine(Frog_Entry());
    }

    private IEnumerator Frog_Entry() {
        Vector2 Frog_Original = character.rectTransform.anchoredPosition;
        Vector2 Frog_New = Frog_Original;
        Frog_New.x = -1170;
        float arc = 75f;

        float duration = 1.1f;
        float t = 0f;
        while (t < duration) {
            float normalizedTime = t / duration;
            float height = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 position = Vector2.Lerp(Frog_Original, Frog_New, normalizedTime);
            position.y += height;
            character.rectTransform.anchoredPosition = position;
            t += Time.deltaTime;
            yield return null;
        }
        character.rectTransform.anchoredPosition = Frog_New;

        yield return new WaitForSeconds(0.25f);

        Vector2 Witch_Original = witch.rectTransform.anchoredPosition;
        Vector2 Witch_New = new Vector2(0f, 500f);
        arc = 500f;

        duration = 1.65f;
        t = 0f;
        while (t < duration) {
            float normalizedTime = t / duration;
            float height = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 position = Vector2.Lerp(Witch_Original, Witch_New, normalizedTime);
            position.y += height;
            witch.rectTransform.anchoredPosition = position;
            t += Time.deltaTime;
            yield return null;
        }
        witch.rectTransform.anchoredPosition = Witch_New;

        Witch_Dialogue();
    }

    private void Witch_Dialogue() {
        names = new string[total];
        sentences = new string[total];

        names[0] = "Witch";
        sentences[0] = "Fine, take your carrots back!\nAs long as you can catch them!!!!!\n\nMUAHAHAHAHAHAHAHAHA!";

        StartCoroutine(BeginDialogue());
    }

    private IEnumerator BeginDialogue() {
        float t = 0f;
        float fadeInDuration = 1.5f;
        Image panelImage = dialogue_panel.GetComponent<Image>();
        Color startColor = new Color(0f, 0f, 0f, 0f);
        Color panel_end = panelImage.color;
        Color text_end = char_name.color;

        dialogue_panel.GetComponent<Image>().color = startColor;
        dialogue_panel.SetActive(true);

        char_name.color = startColor;
        char_name.text = names[counter];

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            panelImage.color = Color.Lerp(startColor, panel_end, t / fadeInDuration);
            char_name.color = Color.Lerp(startColor, text_end, t / fadeInDuration);
            yield return null;
        }

        StartCoroutine(ShowText(sentences[counter]));
        counter += 1;
    }

    private IEnumerator ShowText(string sentence) {
        textbox.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            if (letter == '\n') {
                yield return new WaitForSeconds(0.3f);
            }
            textbox.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(1f);
        dialogue_panel.SetActive(false);
        StartCoroutine(PickupBag());
    }

    private IEnumerator PickupBag() {
        Vector2 Frog_Original = character.rectTransform.anchoredPosition;
        Vector2 Frog_New = character_with_bag.rectTransform.anchoredPosition;

        float duration = 0.25f;
        float t = 0f;
        while (t < duration) {
            float normalizedTime = t / duration;
            Vector2 position = Vector2.Lerp(Frog_Original, Frog_New, normalizedTime);
            character.rectTransform.anchoredPosition = position;
            t += Time.deltaTime;
            yield return null;
        }
        character.rectTransform.anchoredPosition = Frog_New;

        character.gameObject.SetActive(false);
        bag.gameObject.SetActive(false);
        character_with_bag.gameObject.SetActive(true);

        Frog_Original = Frog_New;
        Frog_New = new Vector2(0f, -565f);

        duration = 1.25f;
        t = 0f;
        while (t < duration) {
            float normalizedTime = t / duration;
            Vector2 position = Vector2.Lerp(Frog_Original, Frog_New, normalizedTime);
            character_with_bag.rectTransform.anchoredPosition = position;
            t += Time.deltaTime;
            yield return null;
        }
        character_with_bag.rectTransform.anchoredPosition = Frog_New;

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowControls());
    }

    private IEnumerator ShowControls() {
        left_control_text.text = ((KeyCode)PlayerPrefs.GetInt("Control2")).ToString();
        right_control_text.text = ((KeyCode)PlayerPrefs.GetInt("Control3")).ToString();

        float t = 0f;
        float fadeInDuration = 1.45f;
        Color startColor = new Color(0f, 0f, 0f, 0f);
        Color endColor = objective_text.color;

        objective_text.color = startColor;
        left_control_title.color = startColor;
        left_control_text.color = startColor;
        right_control_title.color = startColor;
        right_control_text.color = startColor;

        controls_panel.SetActive(true);

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            objective_text.color = Color.Lerp(startColor, endColor, t / fadeInDuration);
            left_control_title.color = Color.Lerp(startColor, endColor, t / fadeInDuration);
            left_control_text.color = Color.Lerp(startColor, endColor, t / fadeInDuration);
            right_control_title.color = Color.Lerp(startColor, endColor, t / fadeInDuration);
            right_control_text.color = Color.Lerp(startColor, endColor, t / fadeInDuration);
            yield return null;
        }

        yield return new WaitForSeconds(1.6f);

        t = 0f;
        fadeInDuration = 1.45f;

        float startVolume = intro_music.volume;
        float endVolume = 0.0f;

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            objective_text.color = Color.Lerp(endColor, startColor, t / fadeInDuration);
            left_control_title.color = Color.Lerp(endColor, startColor, t / fadeInDuration);
            left_control_text.color = Color.Lerp(endColor, startColor, t / fadeInDuration);
            right_control_title.color = Color.Lerp(endColor, startColor, t / fadeInDuration);
            right_control_text.color = Color.Lerp(endColor, startColor, t / fadeInDuration);
            intro_music.volume = Mathf.Lerp(startVolume, endVolume, t / fadeInDuration);
            yield return null;
        }

        controls_panel.SetActive(false);
        intro_music.Stop();
        PlayerPrefs.SetInt("Boss_fight_introduction_complete", 1);
    }
}
