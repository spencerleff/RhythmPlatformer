using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Intro_Cutscene_Logic : MonoBehaviour
{
    public GameObject dialogue_panel;
    public TextMeshProUGUI char_name;
    public TextMeshProUGUI textbox;
    public TextMeshProUGUI continue_button_text;

    private string[] names;
    private string[] sentences;
    private int counter = 0;
    private int total = 3;
    private float textSpeed = 0.04f;

    void Start()
    {
        names = new string[total];
        sentences = new string[total];


        //dialogue
        names[0] = "Frog";
        sentences[0] = "Frog is enjoying his day on the moon!";

        names[1] = "Frog";
        sentences[1] = "He loves carrots so much!!!";

        names[2] = "Witch";
        sentences[2] = "I smell carrots!!!!!!! MUAHAHAHAHA";


        StartCoroutine(BeginDialogue());
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


        StartCoroutine(ShowText(sentences[counter]));
        counter += 1;
    }

    private void Continue_Dialogue() {
        //go to next dialogue
        if (counter < total) {
            char_name.text = names[counter];
            StartCoroutine(ShowText(sentences[counter]));
            counter += 1;
        }
        //end of dialogue, run the ending animation
        else {
            dialogue_panel.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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
}
