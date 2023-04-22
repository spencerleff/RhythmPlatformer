using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RollCredits : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI content;

    private bool isShowingCredit = false;

    private IEnumerator ShowCredit(string titleText, string contentText)
    {
        title.text = titleText;
        content.text = contentText;

        //fade in
        float fadeInTime = 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeInTime)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, elapsedTime / fadeInTime);

            title.color = new Color(title.color.r, title.color.g, title.color.b, alpha);
            content.color = new Color(content.color.r, content.color.g, content.color.b, alpha);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        //wait 1.5s
        yield return new WaitForSeconds(1.5f);

        //fade out
        float fadeOutTime = 1.0f;
        elapsedTime = 0.0f;

        while (elapsedTime < fadeOutTime)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeOutTime);

            title.color = new Color(title.color.r, title.color.g, title.color.b, alpha);
            content.color = new Color(content.color.r, content.color.g, content.color.b, alpha);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        isShowingCredit = false;
    }

    void Start()
    {
        // Show the credits one by one
        isShowingCredit = true;
        StartCoroutine(ShowCredit("Credits", ""));
    }

    void Update()
    {
        // Check if a credit is currently being shown
        if (!isShowingCredit)
        {
            // Show the next credit
            if (title.text == "Credits")
            {
                isShowingCredit = true;
                StartCoroutine(ShowCredit("Programming", "Spencer Leff"));
            }
            else if (title.text == "Programming")
            {
                isShowingCredit = true;
                StartCoroutine(ShowCredit("Writer", "Spencer Leff"));
            }
            else if (title.text == "Writer") {
                isShowingCredit = true;
                StartCoroutine(ShowCredit("Director", "Spencer Leff"));
            }
            else if (title.text == "Director")
            {
                isShowingCredit = true;
                StartCoroutine(ShowCredit("Assets", "Spencer Leff\nDALLE 2\nDALLE\nDeep-Fold\nu/8bit_story"));
            }
            else if (title.text == "Assets")
            {
                isShowingCredit = true;
                StartCoroutine(ShowCredit("Music", "Potsu - Letting Go\nPotsu - Breakfast\nSuper Mario 64 - Game Over\nStardew Valley - Cloud Country\nPokemon Pearl - Team Galactic Building\nPokemon Pearl - Rival Battle\nMinecraft - Player Hurt\nMinecraft - Water Bucket"));
            }
            else if (title.text == "Music")
            {
                isShowingCredit = true;
                StartCoroutine(ShowCredit("Thanks for playing!", ""));
            }
            else if (title.text == "Thanks for playing!")
            {
                isShowingCredit = true;
                SceneManager.LoadScene(0);
            }
        }
    }
}
