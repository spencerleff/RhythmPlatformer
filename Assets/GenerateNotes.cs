using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateNotes : MonoBehaviour
{
    public Canvas canvas;
    public Color squareColor = new Color(1f, 0.2f, 1f, 0.5f);
    private float squareSize = 150f;
    private float speed = 750f;
    private float fadeTime = 0.2f;

    private void Start()
    {
        Vector2 Pos1 = new Vector2(-600f, 1000f);
        Vector2 Pos2 = new Vector2(-200f, 1000f);
        Vector2 Pos3 = new Vector2(200f, 1000f);
        Vector2 Pos4 = new Vector2(600f, 1000f);

        StartCoroutine(TimedNoteCreation(Pos1, 2f));
    }

    //create the note at the designated position with a rate of 'speed' and delayed by 'delay' seconds
    private IEnumerator TimedNoteCreation(Vector2 position, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject noteObj = new GameObject("Note");
        Image noteImage = noteObj.AddComponent<Image>();
        noteImage.color = squareColor;
        noteImage.rectTransform.sizeDelta = new Vector2(squareSize, squareSize);
        noteImage.rectTransform.anchoredPosition = position;
        noteObj.transform.SetParent(canvas.transform, false);

        float elapsed = 0f;
        Vector2 startPosition = position;
        Vector2 targetPosition = new Vector2(position.x, -300f);
        
        //move note down at a rate of the 'speed' variable
        while (noteImage.rectTransform.anchoredPosition.y > targetPosition.y)
        {
            float t = elapsed / Mathf.Abs((position.y - targetPosition.y) / speed);
            noteImage.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        //IF NOTE WAS HIT, DO THIS
        while (noteImage.color.a > 0)
        {
            float newAlpha = noteImage.color.a - (Time.deltaTime / fadeTime);
            if (newAlpha < 0f) {
                newAlpha = 0f;
            }
    
            noteImage.color = new Color(noteImage.color.r, noteImage.color.g, noteImage.color.b, newAlpha);
            yield return null;
        }

        
        //Destroy the note - possibly update if on miss needs to rewind.  keep like an array of x amount i guess?
        Destroy(noteObj);
    }

}
