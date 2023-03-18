using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateNotes : MonoBehaviour
{
    public Canvas canvas;
    public Image background;
    public Image player;
    
    private List<GameObject> notesList = new List<GameObject>();
    private Color squareColor = new Color(0.75f, 0.4f, 0.85f, 0.75f);
    private float speed = 600f;
    private float ReverseNoteSpeed = 1800f;
    static float squareSize = 140f;
    static float NoteHitboxSize = 80f;
    private bool shouldMove = true;
    private float FirstNoteStartingY = 1000f;
    private int nextNoteIndex = 0;
    private bool inMissAnimation = false;
    private bool alreadyHopped = false;

    private KeyCode control1;
    private KeyCode control2;
    private KeyCode control3;
    private KeyCode control4;

    public class NoteController : MonoBehaviour
    {
        public bool IsHit { get; set; }
    }

    private void Start()
    {
        player.canvas.sortingOrder = canvas.sortingOrder + 1;

        control1 = (KeyCode)PlayerPrefs.GetInt("Control1");
        control2 = (KeyCode)PlayerPrefs.GetInt("Control2");
        control3 = (KeyCode)PlayerPrefs.GetInt("Control3");
        control4 = (KeyCode)PlayerPrefs.GetInt("Control4");

        //notes for the current song
        Vector2 Pos1 = new Vector2(-600f, 600f);
        Vector2 Pos2 = new Vector2(-200f, 1300f);
        Vector2 Pos3 = new Vector2(200f, 1600f);
        Vector2 Pos4 = new Vector2(600f, 1900f);

        float c1 = -600f;
        float c2 = -200f;
        float c3 = 200f;
        float c4 = 600f;

        //create notes
        CN(c1, FirstNoteStartingY);
        CN(c2, 1500f);
        CN(c1, 2000f);
        CN(c3, 2500f);
        CN(c1, 3000f);
        CN(c4, 3500f);
        CN(c1, 4000f);
        CN(c2, 4500f);
        CN(c1, 5000f);
        CN(c3, 5500f);
        CN(c1, 6000f);
        CN(c4, 6500f);
    }

    //Create Note
    private void CN(float x, float y)
    {
        Vector2 pos = new Vector2(x, y);
        GenerateNotesAtPos(pos);
    }

    private void GenerateNotesAtPos(Vector2 position)
    {
        GameObject noteObj = new GameObject("Note");
        Image noteImage = noteObj.AddComponent<Image>();
        noteImage.color = squareColor;
        noteImage.rectTransform.sizeDelta = new Vector2(squareSize, squareSize);
        noteImage.rectTransform.anchoredPosition = position;
        noteObj.transform.SetParent(canvas.transform, false);

        noteObj.AddComponent<NoteController>().IsHit = false;

        notesList.Add(noteObj);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("PauseActive") == 0) {
            if (shouldMove)
            {
                for (int i = 0; i < notesList.Count; i++)
                {
                    MoveNotes(notesList[i]);
                }
            }

            if (!inMissAnimation && !alreadyHopped) {
                GameObject n_obj = null; 
                for (int i = 0; i < notesList.Count; i++) {
                    n_obj = notesList[i]; 
                    if (n_obj.GetComponent<NoteController>().IsHit)
                    {
                        Image n_img = n_obj.GetComponent<Image>();
                        Vector2 startPosition = player.rectTransform.anchoredPosition;
                        Vector2 targetPosition = new Vector2(n_img.rectTransform.anchoredPosition.x, n_img.rectTransform.anchoredPosition.y + 150);
                        float animationDuration = 0.175f;

                        StartCoroutine(PlayerHopAnimation(startPosition, targetPosition, animationDuration));
                        
                        alreadyHopped = true;
                    }
                }
            }
            else if (!inMissAnimation && alreadyHopped) {
                GameObject n_obj = null; 
                for (int i = 0; i < notesList.Count; i++) {
                    n_obj = notesList[i]; 
                    if (n_obj.GetComponent<NoteController>().IsHit)
                    {
                        Image n_img = n_obj.GetComponent<Image>();
                        Vector2 targetPosition = new Vector2(n_img.rectTransform.anchoredPosition.x, n_img.rectTransform.anchoredPosition.y + 150);
                        player.rectTransform.anchoredPosition = targetPosition;
                    }
                }
            }

            if (Input.GetKeyDown(control1))
            {
                CheckNoteHit(-600);
            }
            else if (Input.GetKeyDown(control2))
            {
                CheckNoteHit(-200);
            }
            else if (Input.GetKeyDown(control3))
            {
                CheckNoteHit(200);
            }
            else if (Input.GetKeyDown(control4))
            {
                CheckNoteHit(600);
            }
        }
    }

    //make the notes begin to fall at the rate of the 'speed' var
    private void MoveNotes(GameObject noteObj)
    {
        Image noteImage = noteObj.GetComponent<Image>();
        noteImage.rectTransform.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);

        //Miss: occurs when a note is not hit and falls below the note hitbox
        if (noteImage.rectTransform.anchoredPosition.y < (-300 - NoteHitboxSize) && !noteObj.GetComponent<NoteController>().IsHit)
        {
            StartCoroutine(NoteMissAnimation(noteObj));
            StartCoroutine(ReverseNotes(notesList, noteObj));
        }
    }

    IEnumerator PlayerHopAnimation(Vector2 startPosition, Vector2 targetPosition, float animationDuration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < animationDuration)
        {
            float t = timeElapsed / animationDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            player.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        player.rectTransform.anchoredPosition = targetPosition;
    }

    private void CheckNoteHit(int hitboxPosition)
    {
        //Level finished
        if (nextNoteIndex >= notesList.Count)
        {
            return;
        }

        GameObject noteObj = notesList[nextNoteIndex];
        NoteController noteController = noteObj.GetComponent<NoteController>();
        if (!noteController.IsHit)
        {
            float distance = Mathf.Abs(noteObj.GetComponent<RectTransform>().anchoredPosition.y + 300f);
            if (distance <= NoteHitboxSize && hitboxPosition == noteObj.GetComponent<RectTransform>().anchoredPosition.x)
            {
                noteController.IsHit = true;
                nextNoteIndex++;
                alreadyHopped = false;
                StartCoroutine(NoteHitAnimation(noteObj));
            }
            else
            {
                StartCoroutine(NoteMissAnimation(noteObj));
                
                if (nextNoteIndex == 0) {
                    StartCoroutine(ReverseNotes(notesList, noteObj));
                }
                else {
                    GameObject prevNote = notesList[nextNoteIndex - 1];
                    StartCoroutine(ReverseNotes(notesList, prevNote));
                }
            }
        }
    }

    private IEnumerator NoteHitAnimation(GameObject noteObj)
    {
        float t = 0f;
        float fadeDuration = 0.5f;
        Image noteImage = noteObj.GetComponent<Image>();
        Color hitColor = squareColor * 0.4f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            noteImage.color = Color.Lerp(squareColor, hitColor, t / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator NoteMissAnimation(GameObject noteObj)
    {
        float t = 0f;
        float fadeDuration = 0.1f;
        Image noteImage = noteObj.GetComponent<Image>();
        Color missColor = new Color(0.9f, 0.2f, 0.2f, 0.75f);


        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            noteImage.color = Color.Lerp(squareColor, missColor, t / fadeDuration);
            yield return null;
        }

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            noteImage.color = Color.Lerp(missColor, squareColor, t / fadeDuration);
            yield return null;
        }

        noteImage.color = squareColor;
    }

    private IEnumerator ReverseNotes(List<GameObject> notesList, GameObject noteObj)
    {
        shouldMove = false;
        inMissAnimation = true;
        yield return new WaitForSeconds(0.5f);

        //find closest note to the missed one on the same x axis value
        GameObject nextNoteObj = null;
        float xPosition = noteObj.GetComponent<RectTransform>().anchoredPosition.x;
        for (int i = notesList.Count - 1; i >= 0; i--)
        {
            GameObject obj = notesList[i];
            float yPosition = obj.GetComponent<RectTransform>().anchoredPosition.y;
            if (xPosition == obj.GetComponent<RectTransform>().anchoredPosition.x && yPosition < noteObj.GetComponent<RectTransform>().anchoredPosition.y)
            {
                nextNoteObj = obj;
                break;
            }
        }

        //reverse until note below reaches hitbox
        if (nextNoteObj != null)
        {
            //set all notes back to original color
            for (int i = 0; i < notesList.Count; i++) {
                notesList[i].GetComponent<Image>().color = squareColor;
            }

            while (nextNoteObj.GetComponent<RectTransform>().anchoredPosition.y <= -300f) {
                for (int i = 0; i < notesList.Count; i++)
                {
                    Image noteImage = notesList[i].GetComponent<Image>();
                    noteImage.rectTransform.anchoredPosition += new Vector2(0, ReverseNoteSpeed * Time.deltaTime);
                    if (player.rectTransform.anchoredPosition.y < -150) {
                        player.rectTransform.anchoredPosition += new Vector2(0, (speed / 6) * Time.deltaTime);
                    }
                }
                yield return new WaitForSeconds(0.0001f);
            }

            //set IsHit of notes above the hitboxes to false
            for (int i = 0; i < notesList.Count; i++) {
                if (notesList[i].GetComponent<RectTransform>().anchoredPosition.y > -290 && notesList[i].GetComponent<NoteController>().IsHit == true) {
                    notesList[i].GetComponent<NoteController>().IsHit = false;
                    nextNoteIndex--;
                }
            }
        }

        //reverse until the first note reaches its starting location
        else {
            //set IsHit of all notes to false, and current note index back to 0
            for (int i = 0; i < notesList.Count; i++) {
                notesList[i].GetComponent<NoteController>().IsHit = false;
                notesList[i].GetComponent<Image>().color = squareColor;
            }
            nextNoteIndex = 0;

            while (notesList[0].GetComponent<RectTransform>().anchoredPosition.y <= FirstNoteStartingY) {
                for (int i = 0; i < notesList.Count; i++)
                {
                    Image noteImage = notesList[i].GetComponent<Image>();
                    noteImage.rectTransform.anchoredPosition += new Vector2(0, ReverseNoteSpeed * Time.deltaTime);
                    if (player.rectTransform.anchoredPosition.y >= -1000) {
                        player.rectTransform.anchoredPosition += new Vector2(0, (speed / -8) * Time.deltaTime);
                    }
                }
                yield return new WaitForSeconds(0.0001f);
            }
        }


        yield return new WaitForSeconds(0.5f);
        shouldMove = true;
        inMissAnimation = false;
    }
}