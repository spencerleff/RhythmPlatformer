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
    }

    IEnumerator hitboxColorAnimation(Image hitbox)
    {
        float time = 0f;
        float duration = 0.2f;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(control1)) {
            StartCoroutine(hitboxColorAnimation(Hitbox1));
        }
        else if (Input.GetKeyDown(control2)) {
            StartCoroutine(hitboxColorAnimation(Hitbox2));
        }
        else if (Input.GetKeyDown(control3)) {
            StartCoroutine(hitboxColorAnimation(Hitbox3));
        }
        else if (Input.GetKeyDown(control4)) {
            StartCoroutine(hitboxColorAnimation(Hitbox4));
        }
    }
}
