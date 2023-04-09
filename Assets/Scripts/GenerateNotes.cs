using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GenerateNotes : MonoBehaviour
{
    public Canvas canvas;
    public RawImage background;
    public RawImage bg1;
    public RawImage bg2;
    public Image player;
    public AudioSource music;
    public Sprite block;
    
    private List<GameObject> notesList = new List<GameObject>();
    private Color squareColor = new Color(1f, 1f, 1f, 1f);
    private float speed = 600f;
    private float ReverseNoteSpeed = 750f;
    static float squareSize = 170f;
    static float NoteHitboxSize = 100f;
    static float middleOfHitbox = -275f;
    private int nextNoteIndex = 0;
    private bool shouldMove = true;
    private bool inMissAnimation = false;
    private bool alreadyHopped = false;
    private bool isLevelCompleted = false;
    private bool isCurrentlyReversing = false;

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

        //generate notes     
        //main melody
        CN(c1, 2250f);
        CN(c3, 2685f);
        CN(c2, 3485f);
        CN(c4, 3890f);
        CN(c3, 4260f);
        CN(c2, 5100f);
        CN(c3, 5395f);
        CN(c1, 5610f);
        CN(c2, 5880f);
        CN(c1, 6510f);
        CN(c2, 6715f);
        CN(c3, 7040f);
        CN(c4, 7310f);

        CN(c3, 8310f);
        CN(c2, 8570f);
        CN(c1, 8800f);
        CN(c3, 9080f);
        CN(c2, 9850f);
        CN(c4, 10280f);
        CN(c3, 10685f);
        CN(c2, 11485f);
        CN(c3, 11815f);
        CN(c1, 12020f);
        CN(c2, 12260f);
        CN(c1, 12880f);
        CN(c2, 13110f);
        CN(c3, 13405f);
        CN(c4, 13665f);

        CN(c3, 14695f);
        CN(c2, 14960f);
        CN(c1, 15240f);
        CN(c3, 15510f);
        CN(c2, 16280f);
        CN(c4, 16700f);
        CN(c3, 17060f);
        CN(c2, 17905f);
        CN(c3, 18230f);
        CN(c1, 18445f);
        CN(c2, 18695f);
        CN(c1, 19270f);
        CN(c2, 19480f);
        CN(c3, 19785f);
        CN(c4, 20015f);

        CN(c3, 21105f);
        CN(c2, 21370f);
        CN(c1, 21610f);
        CN(c3, 21885f);
        CN(c2, 22645f);
        CN(c4, 23070f);
        CN(c3, 23495f);
        CN(c2, 24280f);
        CN(c3, 24615f);
        CN(c1, 24830f);
        CN(c2, 25075f);
        CN(c1, 25715f);
        CN(c2, 25900f);
        CN(c3, 26200f);
        CN(c4, 26455f);

        CN(c3, 27500f);
        CN(c2, 27765f);
        CN(c1, 28005f);

        //second part: quick beats
        CN(c3, 28220f);
        CN(c3, 28480f);
        CN(c2, 28730f);
        CN(c1, 28965f);
        CN(c1, 29185f);
        CN(c1, 29465f);
        CN(c1, 29655f);
        CN(c2, 29875f);
        CN(c3, 30050f);
        CN(c4, 30425f);
        CN(c3, 30610f);
        CN(c3, 30770f);
        CN(c2, 31060f);
        CN(c2, 31230f);


        CN(c3, 31545f);
        CN(c3, 31765f);
        CN(c2, 32090f);
        CN(c1, 32275f);
        CN(c1, 32455f);
        CN(c1, 32700f);
        CN(c1, 32950f);
        CN(c3, 33115f);
        CN(c3, 33310f);
        CN(c4, 33670f);
        CN(c3, 33840f);
        CN(c1, 34035f);
        CN(c3, 34345f);
        CN(c2, 34515f);
        
        //long notes section
        CN(c3, 34730f);
        CN(c2, 35320f);
        CN(c4, 36330f);
        CN(c4, 36940f);

        CN(c3, 37930f);
        CN(c2, 38450f);
        CN(c4, 39480f);
        CN(c4, 40075f);

        CN(c3, 41035f);
        CN(c2, 41630f);
        CN(c4, 42635f);
        CN(c4, 43255f);

        CN(c3, 44245f);
        CN(c2, 44840f);
        CN(c4, 45845f);
        CN(c4, 46485f);

        //final melody
        CN(c1, 47060f);
        CN(c3, 47500f);
        CN(c2, 48305f);
        CN(c4, 48800f);
        CN(c3, 49115f);
        CN(c2, 49905f);
        CN(c3, 50205f);
        CN(c1, 50410f);
        CN(c2, 50675f);
        CN(c1, 51270f);
        CN(c2, 51465f);
        CN(c3, 51740f);
        CN(c4, 52050f);

        CN(c3, 53085f);
        CN(c2, 53350f);
        CN(c1, 53590f);
        CN(c3, 53865f);
        CN(c2, 54630f);
        CN(c4, 55050f);
        CN(c3, 55480f);
        CN(c2, 56260f);
        CN(c3, 56595f);
        CN(c1, 56810f);
        CN(c2, 57060f);
        CN(c1, 57660f);
        CN(c2, 57890f);
        CN(c3, 58230f);
        CN(c4, 58445f);
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
        noteImage.sprite = block;
        noteImage.rectTransform.sizeDelta = new Vector2(squareSize, squareSize);
        noteImage.rectTransform.anchoredPosition = position;
        noteObj.transform.SetParent(canvas.transform, false);

        noteObj.AddComponent<NoteController>().IsHit = false;

        notesList.Add(noteObj);
    }

    private void Update()
    {
        if (nextNoteIndex >= notesList.Count)
        {
            if (!isLevelCompleted) {
                Debug.Log("You Win!");
                shouldMove = false;
                StartCoroutine(CompletedLevel());
                isLevelCompleted = true;
            }
        }

        if (PlayerPrefs.GetInt("PauseActive") == 0) {
            if (shouldMove)
            {
                for (int i = 0; i < notesList.Count; i++)
                {
                    MoveNotes(notesList[i]);
                }
            }
            
            bg1.uvRect = new Rect(bg1.uvRect.x, bg1.uvRect.y + 0.02f * Time.deltaTime, bg1.uvRect.width, bg1.uvRect.height);
            bg2.uvRect = new Rect(bg2.uvRect.x, bg2.uvRect.y + 0.01f * Time.deltaTime, bg2.uvRect.width, bg2.uvRect.height);
        }
        
        if (PlayerPrefs.GetInt("CountdownActive") == 0) {
            if (PlayerPrefs.GetInt("PauseActive") == 0) {
                if (!inMissAnimation && !alreadyHopped) {
                    GameObject n_obj = null; 
                    for (int i = 0; i < notesList.Count; i++) {
                        n_obj = notesList[i]; 
                        if (n_obj.GetComponent<NoteController>().IsHit)
                        {
                            Image n_img = n_obj.GetComponent<Image>();
                            Vector2 startPosition = player.rectTransform.anchoredPosition;
                            Vector2 targetPosition = new Vector2(n_img.rectTransform.anchoredPosition.x, n_img.rectTransform.anchoredPosition.y + 150);
                            float animationDuration = 0.26f;

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

                if (!isCurrentlyReversing) {
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
        }
    }

    private IEnumerator CompletedLevel()
    {
        float startVolume = music.volume;
        float music_fade_duration = 2f;
        float timePassed = 0f;

        while (music.volume > 0)
        {
            timePassed += Time.deltaTime;
            float fractionCompleted = timePassed / music_fade_duration;
            music.volume = Mathf.Lerp(startVolume, 0, fractionCompleted);
            yield return null;
        }

        music.Stop();
        SceneManager.LoadScene(0);
    }

    //make the notes begin to fall at the rate of the 'speed' var
    private void MoveNotes(GameObject noteObj)
    {
        Image noteImage = noteObj.GetComponent<Image>();
        noteImage.rectTransform.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);

        //Miss: occurs when a note is not hit and falls below the note hitbox
        if (noteImage.rectTransform.anchoredPosition.y < (middleOfHitbox - NoteHitboxSize) && !noteObj.GetComponent<NoteController>().IsHit)
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
        GameObject noteObj = notesList[nextNoteIndex];
        NoteController noteController = noteObj.GetComponent<NoteController>();
        if (!noteController.IsHit)
        {
            float distance = Mathf.Abs(noteObj.GetComponent<RectTransform>().anchoredPosition.y + 275f);
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
        float fadeDuration = 0.35f;
        Image noteImage = noteObj.GetComponent<Image>();
        Color hitColor = squareColor * 0.3f;

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

        //fade back to normal color, just in case player misses and it rewinds
        while (t < (fadeDuration / 2))
        {
            t += Time.deltaTime;
            noteImage.color = Color.Lerp(missColor, squareColor, t / fadeDuration);
            yield return null;
        }

        noteImage.color = squareColor;
    }

    private IEnumerator ReverseNotes(List<GameObject> notesList, GameObject noteObj)
    {
        isCurrentlyReversing = true;
        shouldMove = false;
        inMissAnimation = true;
        music.Pause();

        yield return new WaitForSeconds(0.5f);

        float playbackPosition = music.time;
        music.pitch = -1.25f;
        music.time = playbackPosition;
        music.Play();

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

        //set all note colors back to original color
        for (int i = notesList.Count - 1; i >= 0; i--)
        {
            notesList[i].GetComponent<Image>().color = squareColor;
        }

        //reverse until note below reaches hitbox
        if (nextNoteObj != null)
        {
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
            nextNoteObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(nextNoteObj.GetComponent<RectTransform>().anchoredPosition.x, -300);

            //set IsHit of notes above the hitboxes to false
            for (int i = 0; i < notesList.Count; i++) {
                if (notesList[i].GetComponent<RectTransform>().anchoredPosition.y > (middleOfHitbox - 10f) && notesList[i].GetComponent<NoteController>().IsHit == true) {
                    notesList[i].GetComponent<NoteController>().IsHit = false;
                    nextNoteIndex--;
                }
            }

            music.Pause();

            yield return new WaitForSeconds(0.5f);

            isCurrentlyReversing = false;
            shouldMove = true;
            inMissAnimation = false;
            music.pitch = 1f;
            music.Play();
        }

        //reverse until the first note reaches its starting location
        else {
            //set IsHit of all notes to false, and current note index back to 0
            for (int i = 0; i < notesList.Count; i++) {
                notesList[i].GetComponent<NoteController>().IsHit = false;
            }
            nextNoteIndex = 0;

            while (notesList[0].GetComponent<RectTransform>().anchoredPosition.y <= 2250) {
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

            //set all note colors back to original color
            for (int i = notesList.Count - 1; i >= 0; i--)
            {
                notesList[i].GetComponent<Image>().color = squareColor;
            }

            yield return new WaitForSeconds(0.5f);

            isCurrentlyReversing = false;
            shouldMove = true;
            inMissAnimation = false;
            music.pitch = 1f;
            music.Pause();

            yield return new WaitForSeconds(3.7f);
            music.time = 0f;
            music.Play();
        }
    }
}