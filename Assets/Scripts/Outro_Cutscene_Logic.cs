using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Outro_Cutscene_Logic : MonoBehaviour
{
    public Image character;
    public GameObject dialogue_panel;
    public TextMeshProUGUI char_name;
    public TextMeshProUGUI textbox;
    public AudioSource intro_music;

    private string[] names;
    private string[] sentences;
    private int counter = 0;
    private int total = 1;
    private float textSpeed = 0.04f;

    void Start()
    {
        intro_music.volume = PlayerPrefs.GetFloat("Volume");
        intro_music.Play();

        names = new string[total];
        sentences = new string[total];

        //dialogue
        names[0] = "Frog";
        sentences[0] = "I got my carrots back!\nYippee!!!!!!";

        StartCoroutine(BeginDialogue());
        StartCoroutine(FrogAnimation());
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
        // Clear the textbox before showing the new sentence
        textbox.text = "";

        // Loop through each character in the sentence and gradually display them
        foreach (char letter in sentence.ToCharArray()) {
            textbox.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private IEnumerator FrogAnimation() {
        Vector2 Frog_Original = new Vector2(3400f, 235f);
        Vector2 Frog_New = new Vector2(2300f, 235f);

        float arc = 500;

        float t = 0f;
        float duration = 3.25f;
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

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}