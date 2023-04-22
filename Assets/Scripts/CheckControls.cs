using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckControls : MonoBehaviour
{
    private KeyCode control1;
    private KeyCode control2;
    private KeyCode control3;
    private KeyCode control4;

    public Image Hitbox1;
    public Image Hitbox2;
    public Image Hitbox3;
    public Image Hitbox4;

    public TextMeshProUGUI Hitbox1Control;
    public TextMeshProUGUI Hitbox2Control;
    public TextMeshProUGUI Hitbox3Control;
    public TextMeshProUGUI Hitbox4Control;

    
    // Start is called before the first frame update
    void Start()
    {
        control1 = (KeyCode)PlayerPrefs.GetInt("Control1");
        control2 = (KeyCode)PlayerPrefs.GetInt("Control2");
        control3 = (KeyCode)PlayerPrefs.GetInt("Control3");
        control4 = (KeyCode)PlayerPrefs.GetInt("Control4");

        Hitbox1Control.text = control1.ToString();
        Hitbox2Control.text = control2.ToString();
        Hitbox3Control.text = control3.ToString();
        Hitbox4Control.text = control4.ToString();

        StartCoroutine(HitboxControlAnimation());
        StartCoroutine(HitboxTextAnimation());
    }

    IEnumerator hitboxColorAnimation(Image hitbox, float duration)
    {
        float time = 0f;
        Color32 startColor = new Color32(255, 255, 255, 190);
        Color32 endColor = new Color32(255, 255, 255, 102);

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = time / duration;

            hitbox.color = Color32.Lerp(startColor, endColor, alpha);

            yield return null;
        }

        hitbox.color = endColor;
    }

    private IEnumerator HitboxControlAnimation() {
        StartCoroutine(hitboxColorAnimation(Hitbox1, 0.5f));
        yield return new WaitForSeconds(0.33f);
        StartCoroutine(hitboxColorAnimation(Hitbox2, 0.5f));
        yield return new WaitForSeconds(0.33f);
        StartCoroutine(hitboxColorAnimation(Hitbox3, 0.5f));
        yield return new WaitForSeconds(0.33f);
        StartCoroutine(hitboxColorAnimation(Hitbox4, 0.5f));
        yield return new WaitForSeconds(0.33f);
    }

    private IEnumerator FontSizeAnimation(TextMeshProUGUI text, float duration) {
        float timeElapsed = 0f;
        float initialSize = 36f;
        float finalSize = 46f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            text.fontSize = Mathf.Lerp(initialSize, finalSize, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            text.fontSize = Mathf.Lerp(finalSize, initialSize, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator HitboxTextAnimation() {
        StartCoroutine(FontSizeAnimation(Hitbox1Control, 0.175f));
        yield return new WaitForSeconds(0.33f);
        Hitbox1Control.faceColor = new Color32(255, 255, 255, 175);
        
        StartCoroutine(FontSizeAnimation(Hitbox2Control, 0.175f));
        yield return new WaitForSeconds(0.33f);
        Hitbox2Control.faceColor = new Color32(255, 255, 255, 175);
        
        StartCoroutine(FontSizeAnimation(Hitbox3Control, 0.175f));
        yield return new WaitForSeconds(0.33f);
        Hitbox3Control.faceColor = new Color32(255, 255, 255, 175);

        StartCoroutine(FontSizeAnimation(Hitbox4Control, 0.175f));
        yield return new WaitForSeconds(0.33f);
        Hitbox4Control.faceColor = new Color32(255, 255, 255, 175);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(control1)) {
            StartCoroutine(hitboxColorAnimation(Hitbox1, 0.2f));
        }
        else if (Input.GetKeyDown(control2)) {
            StartCoroutine(hitboxColorAnimation(Hitbox2, 0.2f));
        }
        else if (Input.GetKeyDown(control3)) {
            StartCoroutine(hitboxColorAnimation(Hitbox3, 0.2f));
        }
        else if (Input.GetKeyDown(control4)) {
            StartCoroutine(hitboxColorAnimation(Hitbox4, 0.2f));
        }
    }
}
