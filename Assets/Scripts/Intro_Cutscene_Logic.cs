using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Intro_Cutscene_Logic : MonoBehaviour
{
    public Image character;
    public Image witch;
    public Image carrots;
    public Image witch_with_carrots;
    public GameObject dialogue_panel;
    public TextMeshProUGUI char_name;
    public TextMeshProUGUI textbox;
    public Button continue_button;
    public TextMeshProUGUI continue_button_text;
    public Button skip_button;
    public TextMeshProUGUI skip_button_text;
    public AudioSource intro_music;
    public AudioSource boss_music;

    private string[] names;
    private string[] sentences;
    private int counter = 0;
    private int total = 4;
    private float textSpeed = 0.03f;
    private bool canContinue = true;

    void Start()
    {
        StartCoroutine(PreloadNextScene());
        
        intro_music.volume = PlayerPrefs.GetFloat("Volume");
        boss_music.volume = PlayerPrefs.GetFloat("Volume");

        intro_music.Play();

        names = new string[total];
        sentences = new string[total];

        //dialogue
        names[0] = "Frog";
        sentences[0] = "What a beautiful day on the moon!";

        names[1] = "Frog";
        sentences[1] = "And I have enough carrots for the entire year!!!";

        names[2] = "Witch";
        sentences[2] = "I smell carrots!!!!!!!\n\nMUAHAHAHAHA";

        names[3] = "Frog";
        sentences[3] = "Come back here!!!";

        StartCoroutine(BeginDialogue());
    }

    //preload the next scene to help synchronize note timings
    private IEnumerator PreloadNextScene() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                StartCoroutine(FadeInSkipButton());
                skip_button.gameObject.SetActive(true);
                yield break;
            }
            yield return null;
        }
    }
    
    private IEnumerator FadeInSkipButton() {
        float t = 0f;
        float fadeInDuration = 1f;
        Color startColor = new Color(0f, 0f, 0f, 0f);
        Color text_end = skip_button_text.color;

        skip_button_text.color = startColor;
        skip_button.gameObject.SetActive(true);

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            skip_button_text.color = Color.Lerp(startColor, text_end, t / fadeInDuration);
            yield return null;
        }
    }

    //fade in the dialogue box, and play the first line of text
    private IEnumerator BeginDialogue() {
        float t = 0f;
        float fadeInDuration = 1.5f;
        Image panelImage = dialogue_panel.GetComponent<Image>();
        Color startColor = new Color(0f, 0f, 0f, 0f);
        Color panel_end = panelImage.color;
        Color text_end = char_name.color;

        dialogue_panel.GetComponent<Image>().color = startColor;
        dialogue_panel.SetActive(true);

        continue_button_text.color = startColor;

        char_name.color = startColor;
        char_name.text = names[counter];

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            panelImage.color = Color.Lerp(startColor, panel_end, t / fadeInDuration);
            continue_button_text.color = Color.Lerp(startColor, text_end, t / fadeInDuration);
            char_name.color = Color.Lerp(startColor, text_end, t / fadeInDuration);
            yield return null;
        }

        canContinue = false;
        StartCoroutine(ShowText(sentences[counter]));
        counter += 1;
    }

    private void Continue_Dialogue() {
        //only continue if the current dialogue is finished
        if (canContinue && counter < total) {
            canContinue = false;
            char_name.text = names[counter];
            StartCoroutine(ShowText(sentences[counter]));
            counter += 1;
        }
    }

    private IEnumerator ShowText(string sentence) {
        //remove continue button, and play rest of animations
        if (counter == 2) {
            intro_music.Stop();
            continue_button.gameObject.SetActive(false);
            boss_music.Play();
            StartCoroutine(WitchAnimation());
        }

        // Clear the textbox before showing the new sentence
        textbox.text = "";

        // Loop through each character in the sentence and gradually display them
        foreach (char letter in sentence.ToCharArray()) {
            textbox.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        canContinue = true;
    }

    private IEnumerator WitchAnimation() {
        Vector2 Witch_Original = new Vector2(2780f, 720f);
        Vector2 Witch_New = new Vector2(carrots.rectTransform.position.x - 10f, carrots.rectTransform.position.y + 75f);
        Vector2 Witch_End = new Vector2(4133f, 720f);

        float arc = 100f;

        float t = 0f;
        float duration = 2.5f;
        while (t < duration) {
            t += Time.deltaTime;
            float normalizedTime = t / duration;

            float x = Mathf.Lerp(Witch_Original.x, Witch_New.x, normalizedTime);
            float y = Mathf.Lerp(Witch_Original.y, Witch_New.y, normalizedTime);
            float z = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 Witch_Pos = new Vector2(x, y + z);

            witch.rectTransform.position = Witch_Pos;
            yield return null;
        }

        witch.rectTransform.position = Witch_New;
        witch.gameObject.SetActive(false);
        carrots.gameObject.SetActive(false);
        witch_with_carrots.gameObject.SetActive(true);


        yield return new WaitForSeconds(0.75f);

        arc = 250f;

        t = 0f;
        while (t < duration) {
            t += Time.deltaTime;
            float normalizedTime = t / duration;

            float x = Mathf.Lerp(Witch_New.x, Witch_End.x, normalizedTime);
            float y = Mathf.Lerp(Witch_New.y, Witch_End.y, normalizedTime);
            float z = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 Witch_Pos = new Vector2(x, y + z);

            witch_with_carrots.rectTransform.position = Witch_Pos;
            yield return null;
        }

        witch_with_carrots.rectTransform.position = Witch_End;

        char_name.text = names[counter];
        StartCoroutine(ShowText(sentences[counter]));
        StartCoroutine(FrogAnimation());
    }

    private IEnumerator FrogAnimation() {
        Vector2 Frog_Original = new Vector2(841f, 291f);
        Vector2 Frog_New = new Vector2(1541f, 291f);

        float arc = 400f;

        float t = 0f;
        float duration = 1.4f;
        while (t < duration) {
            t += Time.deltaTime;
            float normalizedTime = t / duration;

            float x = Mathf.Lerp(Frog_Original.x, Frog_New.x, normalizedTime);
            float y = Mathf.Lerp(Frog_Original.y, Frog_New.y, normalizedTime);
            float z = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 Frog_Pos = new Vector2(x, y + z);

            character.rectTransform.position = Frog_Pos;
            yield return null;
        }

        character.rectTransform.position = Frog_New;

        yield return new WaitForSeconds(0.2f);

        Frog_Original = Frog_New;
        Frog_New = new Vector2(2241f, 291f);

        t = 0f;
                while (t < duration) {
            t += Time.deltaTime;
            float normalizedTime = t / duration;

            float x = Mathf.Lerp(Frog_Original.x, Frog_New.x, normalizedTime);
            float y = Mathf.Lerp(Frog_Original.y, Frog_New.y, normalizedTime);
            float z = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 Frog_Pos = new Vector2(x, y + z);

            character.rectTransform.position = Frog_Pos;
            yield return null;
        }

        character.rectTransform.position = Frog_New;

        yield return new WaitForSeconds(0.2f);

        Frog_Original = Frog_New;
        Frog_New = new Vector2(4133f, 291f);

        t = 0f;
        duration = 1f;
        while (t < duration) {
            t += Time.deltaTime;
            float normalizedTime = t / duration;

            float x = Mathf.Lerp(Frog_Original.x, Frog_New.x, normalizedTime);
            float y = Mathf.Lerp(Frog_Original.y, Frog_New.y, normalizedTime);
            float z = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 Frog_Pos = new Vector2(x, y + z);

            character.rectTransform.position = Frog_Pos;
            yield return null;
        }

        character.rectTransform.position = Frog_New;

        //end cutscene
        dialogue_panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}