using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GenerateNotes : MonoBehaviour
{
    public Canvas canvas;
    public RawImage background;
    public RawImage bg1;
    public RawImage bg2;
    public Image player;
    public Image witch;
    public AudioSource music;
    public AudioSource gameOverSound;
    public AudioSource healSound;
    public AudioSource hurtSound;
    public Sprite block;
    public Sprite floor;
    public Sprite heart_full;
    public Sprite heart_half;
    public Sprite heart_empty;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public TextMeshProUGUI completion;
    public TextMeshProUGUI combo;
    public GameObject gameOverMenu;
    public Image Hitbox1;
    public Image Hitbox2;
    public Image Hitbox3;
    public Image Hitbox4;
    public TextMeshProUGUI Hitbox1Control;
    public TextMeshProUGUI Hitbox2Control;
    public TextMeshProUGUI Hitbox3Control;
    public TextMeshProUGUI Hitbox4Control;
    
    private List<GameObject> notesList = new List<GameObject>();
    private Color squareColor = new Color(1f, 1f, 1f, 1f);
    private Color textColor = new Color(1f, 1f, 1f, 0.70588f);
    private float speed = 600f;
    private float ReverseNoteSpeed = 750f;
    static float squareSize = 170f;
    static float floorWidth = 250f;
    static float floorHeight = 145f;
    static float floorScale = 10.25f;
    static float NoteHitboxSize = 95f;
    static float middleOfHitbox = -275f;
    private int nextNoteIndex = 0;
    private int totalNotes = 132;
    private bool shouldMove = true;
    private bool inMissAnimation = false;
    private bool alreadyHopped = false;
    private bool isLevelCompleted = false;
    private bool isCurrentlyReversing = false;
    private bool gameOver = false;

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
        PlayerPrefs.SetInt("Health", 6);

        control1 = (KeyCode)PlayerPrefs.GetInt("Control1");
        control2 = (KeyCode)PlayerPrefs.GetInt("Control2");
        control3 = (KeyCode)PlayerPrefs.GetInt("Control3");
        control4 = (KeyCode)PlayerPrefs.GetInt("Control4");

        if (PlayerPrefs.GetInt("Extreme_Active") == 1) {
            speed *= 1.2f;
            ReverseNoteSpeed *= 1.2f;
        }

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
        CN(c1, 2245f);
        CN(c3, 2680f);
        CN(c2, 3487f);
        CN(c4, 3890f);
        CN(c3, 4260f);
        CN(c2, 5095f);
        CN(c3, 5385f);
        CN(c1, 5605f);
        CN(c2, 5875f);
        CN(c1, 6500f);
        CN(c2, 6700f);
        CN(c3, 7030f);
        CN(c4, 7300f);

        CN(c3, 8310f);
        CN(c2, 8570f);
        CN(c1, 8800f);
        CN(c3, 9085f);
        CN(c2, 9850f);
        CN(c4, 10280f);
        CN(c3, 10685f);
        CN(c2, 11485f);
        CN(c3, 11815f);
        CN(c1, 12025f);
        CN(c2, 12260f);
        CN(c1, 12875f);
        CN(c2, 13115f);
        CN(c3, 13410f);
        CN(c4, 13665f);

        CN(c3, 14695f);
        CN(c2, 14965f);
        CN(c1, 15237f);
        CN(c3, 15510f);
        CN(c2, 16280f);
        CN(c4, 16700f);
        CN(c3, 17070f);
        CN(c2, 17910f);
        CN(c3, 18235f);
        CN(c1, 18450f);
        CN(c2, 18700f);
        CN(c1, 19275f);
        CN(c2, 19485f);
        CN(c3, 19790f);
        CN(c4, 20020f);

        CN(c3, 21105f);
        CN(c2, 21370f);
        CN(c1, 21610f);
        CN(c3, 21885f);
        CN(c2, 22655f);
        CN(c4, 23075f);
        CN(c3, 23495f);
        CN(c2, 24280f);
        CN(c3, 24615f);
        CN(c1, 24830f);
        CN(c2, 25075f);
        CN(c1, 25720f);
        CN(c2, 25905f);
        CN(c3, 26205f);
        CN(c4, 26455f);

        CN(c3, 27505f);
        CN(c2, 27770f);
        CN(c1, 28010f);

        //second part: quick beats
        CN(c3, 28210f);
        CN(c3, 28485f);
        CN(c2, 28735f);
        CN(c1, 28965f);
        CN(c1, 29180f);
        CN(c1, 29465f);
        CN(c1, 29655f);
        CN(c2, 29885f);
        CN(c3, 30050f);
        CN(c4, 30425f);
        CN(c3, 30605f);
        CN(c3, 30775f);
        CN(c2, 31060f);
        CN(c2, 31235f);

        CN(c3, 31498f);
        CN(c3, 31723f);
        CN(c2, 32063f);
        CN(c1, 32258f);
        CN(c1, 32445f);
        CN(c1, 32685f);
        CN(c1, 32855f);
        CN(c3, 33105f);
        CN(c3, 33300f);
        CN(c4, 33660f);
        CN(c3, 33835f);
        CN(c1, 34030f);
        CN(c3, 34340f);
        CN(c2, 34505f);
        
        //long notes section
        CN(c3, 34725f);
        CN(c2, 35320f);
        CN(c4, 36325f);
        CN(c4, 36930f);

        CN(c3, 37925f);
        CN(c2, 38460f);
        CN(c4, 39480f);
        CN(c4, 40075f);

        CN(c3, 41035f);
        CN(c2, 41645f);
        CN(c4, 42635f);
        CN(c4, 43255f);

        CN(c3, 44250f);
        CN(c2, 44843f);
        CN(c4, 45845f);
        CN(c4, 46490f);

        //final melody
        CN(c1, 47060f);
        CN(c3, 47500f);
        CN(c2, 48295f);
        CN(c4, 48755f);
        CN(c3, 49105f);
        CN(c2, 49910f);
        CN(c3, 50205f);
        CN(c1, 50420f);
        CN(c2, 50675f);
        CN(c1, 51275f);
        CN(c2, 51470f);
        CN(c3, 51745f);
        CN(c4, 52055f);

        CN(c3, 53090f);
        CN(c2, 53355f);
        CN(c1, 53595f);
        CN(c3, 53870f);
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
        CN(c4, 58450f);
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
                shouldMove = false;
                StartCoroutine(CompletedLevel());
                isLevelCompleted = true;
            }
        }

        if (PlayerPrefs.GetInt("PauseActive") == 0 && !gameOver) {
            if (shouldMove && !isCurrentlyReversing)
            {
                for (int i = 0; i < notesList.Count; i++)
                {
                    MoveNotes(notesList[i]);
                }
            }
            
            bg1.uvRect = new Rect(bg1.uvRect.x, bg1.uvRect.y + 0.04f * Time.deltaTime, bg1.uvRect.width, bg1.uvRect.height);
            bg2.uvRect = new Rect(bg2.uvRect.x, bg2.uvRect.y + 0.02f * Time.deltaTime, bg2.uvRect.width, bg2.uvRect.height);
        }
        
        if (PlayerPrefs.GetInt("CountdownActive") == 0) {
            if (PlayerPrefs.GetInt("PauseActive") == 0 && !gameOver) {
                if (!inMissAnimation && !alreadyHopped) {
                    GameObject n_obj = null; 
                    for (int i = 0; i < notesList.Count; i++) {
                        n_obj = notesList[i]; 
                        if (n_obj.GetComponent<NoteController>().IsHit)
                        {
                            Image n_img = n_obj.GetComponent<Image>();
                            Vector2 startPosition = player.rectTransform.anchoredPosition;
                            Vector2 targetPosition = new Vector2(n_img.rectTransform.anchoredPosition.x, n_img.rectTransform.anchoredPosition.y + 100);

                            StartCoroutine(PlayerHopAnimation(startPosition, targetPosition));
                            
                            alreadyHopped = true;
                        }
                    }
                }
                else if (!inMissAnimation && alreadyHopped && !isLevelCompleted) {
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

                if (!isCurrentlyReversing && !isLevelCompleted) {
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
        //fade in the exit platform
        StartCoroutine(LoadInExitPlatform(new Vector2(2300, -300f)));

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
        StartCoroutine(GameWinHopAnimation());
    }

    //make the notes begin to fall at the rate of the 'speed' var
    private void MoveNotes(GameObject noteObj)
    {
        Image noteImage = noteObj.GetComponent<Image>();
        noteImage.rectTransform.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);

        //Miss: occurs when a note is not hit and falls below the note hitbox
        if (noteImage.rectTransform.anchoredPosition.y < (middleOfHitbox - NoteHitboxSize) && !noteObj.GetComponent<NoteController>().IsHit)
        {
            combo.text = "0";
            combo.color = textColor;
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

    IEnumerator PlayerHopAnimation(Vector2 startPosition, Vector2 targetPosition)
    {
        float timeElapsed = 0f;
        float animationDuration = 0.28f;
        Vector2 peakPosition = new Vector2((startPosition.x + targetPosition.x) / 2, startPosition.y + Mathf.Abs(targetPosition.x - startPosition.x) * 0.2f);

        while (timeElapsed < animationDuration)
        {
            float t = timeElapsed / animationDuration;
            float smoothT = Mathf.SmoothStep(0, 1, t);
            
            Vector2 currentPos = Vector2.Lerp(startPosition, targetPosition, smoothT);
            currentPos.y += Mathf.Sin(smoothT * Mathf.PI) * (peakPosition.y - startPosition.y);

            player.rectTransform.anchoredPosition = currentPos;

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
                combo.text = (int.Parse(combo.text) + 1).ToString();
                //combo > 125
                if (int.Parse(combo.text) > 125 && int.Parse(combo.text) % 25 == 0) {
                    GainHealth();
                }
                //combo - 125
                else if (int.Parse(combo.text) == 125) {
                    combo.color = new Color32(255, 0, 0, 255);
                    GainHealth();
                }
                //combo - 100
                else if (int.Parse(combo.text) == 100) {
                    combo.color = new Color32(255, 150, 0, 255);
                    GainHealth();
                }
                //combo - 75
                else if (int.Parse(combo.text) == 75) {
                    combo.color = new Color32(255, 255, 0, 255);
                    GainHealth();
                }
                //combo - 50
                else if (int.Parse(combo.text) == 50) {
                    combo.color = new Color32(0, 255, 0, 255);
                    GainHealth();
                }
                //combo - 25
                else if (int.Parse(combo.text) == 25) {
                    combo.color = new Color32(0, 150, 0, 255);
                    GainHealth();
                }
                UpdateCompletionPercentage();
                nextNoteIndex++;
                alreadyHopped = false;
                StartCoroutine(NoteHitAnimation(noteObj));
            }
            else
            {
                combo.text = "0";
                combo.color = textColor;
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

    private void UpdateCompletionPercentage() {
        float percentage = (nextNoteIndex - 1) * 100f / totalNotes;
        if (percentage < 0) {
            percentage = 0;
        }
        else if (percentage > 99) {
            percentage = 100;
        }
        completion.text = $"{percentage:F0}%";
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

    private IEnumerator ReverseNotes(List<GameObject> notesList, GameObject noteObj)
    {
        isCurrentlyReversing = true;
        shouldMove = false;
        inMissAnimation = true;
        music.Pause();

        LoseHealth();

        yield return new WaitForSeconds(0.5f);

        float playbackPosition = music.time;
        
        if (PlayerPrefs.GetInt("Extreme_Active") == 1) {
            music.pitch = (-1.25f * 1.2f);
        }
        else {
            music.pitch = -1.25f;
        }

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
                bg1.uvRect = new Rect(bg1.uvRect.x, bg1.uvRect.y - 0.4f * Time.deltaTime, bg1.uvRect.width, bg1.uvRect.height);
                bg2.uvRect = new Rect(bg2.uvRect.x, bg2.uvRect.y - 0.2f * Time.deltaTime, bg2.uvRect.width, bg2.uvRect.height);

                //move player to correct position after falling
                Image noteImage = nextNoteObj.GetComponent<Image>();
                if (player.rectTransform.anchoredPosition.y < -150) {
                    player.rectTransform.anchoredPosition += new Vector2(0, (speed / 1.5f) * Time.deltaTime);
                }

                for (int i = 0; i < notesList.Count; i++)
                {
                    noteImage = notesList[i].GetComponent<Image>();
                    noteImage.rectTransform.anchoredPosition += new Vector2(0, ReverseNoteSpeed * Time.deltaTime);
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
            UpdateCompletionPercentage();

            yield return new WaitForSeconds(0.5f);

            isCurrentlyReversing = false;
            shouldMove = true;
            inMissAnimation = false;

            if (PlayerPrefs.GetInt("Extreme_Active") == 1) {
                music.pitch = 1.2f;
            }
            else {
                music.pitch = 1f;
            }
            
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
                bg1.uvRect = new Rect(bg1.uvRect.x, bg1.uvRect.y - 0.8f * Time.deltaTime, bg1.uvRect.width, bg1.uvRect.height);
                bg2.uvRect = new Rect(bg2.uvRect.x, bg2.uvRect.y - 0.4f * Time.deltaTime, bg2.uvRect.width, bg2.uvRect.height);
                
                for (int i = 0; i < notesList.Count; i++)
                {
                    Image noteImage = notesList[i].GetComponent<Image>();
                    noteImage.rectTransform.anchoredPosition += new Vector2(0, ReverseNoteSpeed * Time.deltaTime);
                    if (player.rectTransform.anchoredPosition.y >= -1000) {
                        player.rectTransform.anchoredPosition += new Vector2(0, (speed * -0.005f) * Time.deltaTime);
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

            if (PlayerPrefs.GetInt("Extreme_Active") == 1) {
                music.pitch = 1.2f;
            }
            else {
                music.pitch = 1f;
            }

            music.Pause();
            UpdateCompletionPercentage();
            StartCoroutine(HitboxControlAnimation());
            StartCoroutine(HitboxTextAnimation());

            if (PlayerPrefs.GetInt("Extreme_Active") == 1) {
                yield return new WaitForSeconds((3.7f / 1.2f));
            }
            else {
                yield return new WaitForSeconds(3.7f);
            }

            music.time = 0f;
            music.Play();
        }
    }

    private void LoseHealth() {
        hurtSound.Play();
        int health = PlayerPrefs.GetInt("Health");
        health -= 1;
        PlayerPrefs.SetInt("Health", health);

        //game over
        if (health == 0) {
            heart1.sprite = heart_empty;
            GameOver();
        }
        else if (health == 1) {
            heart1.sprite = heart_half;
        }
        else if (health == 2) {
            heart2.sprite = heart_empty;
        }
        else if (health == 3) {
            heart2.sprite = heart_half;
        }
        else if (health == 4) {
            heart3.sprite = heart_empty;
        }
        else if (health == 5) {
            heart3.sprite = heart_half;
        }
    }

    private void GainHealth() {
        int health = PlayerPrefs.GetInt("Health");
        if (health >= 6) {
            return;
        }

        health += 1;
        PlayerPrefs.SetInt("Health", health);

        if (health == 2) {
            healSound.Play();
            heart1.sprite = heart_full;
        }
        else if (health == 3) {
            healSound.Play();
            heart2.sprite = heart_half;
        }
        else if (health == 4) {
            healSound.Play();
            heart2.sprite = heart_full;
        }
        else if (health == 5) {
            healSound.Play();
            heart3.sprite = heart_half;
        }
        else if (health == 6) {
            healSound.Play();
            heart3.sprite = heart_full;
        }
    }

    private void GameOver() {
        gameOver = true;
        StopAllCoroutines();
        for (int i = notesList.Count - 1; i >= 0; i--)
        {
            notesList[i].GetComponent<Image>().color = squareColor * 0.3f;
        }

        gameOverMenu.SetActive(true);
        gameOverSound.Play();
    }

    private IEnumerator GameWinHopAnimation() {
        Vector2 Frog_Original = player.rectTransform.anchoredPosition;
        Vector2 Frog_New = Frog_Original;
        Frog_New.x += 600;
        float arc = 275f;

        float duration = 1f;
        float t = 0f;
        while (t < duration) {
            float normalizedTime = t / duration;
            float height = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 position = Vector2.Lerp(Frog_Original, Frog_New, normalizedTime);
            position.y += height;
            player.rectTransform.anchoredPosition = position;
            t += Time.deltaTime;
            yield return null;
        }
        player.rectTransform.anchoredPosition = Frog_New;

        yield return new WaitForSeconds(0.25f);

        Vector2 Frog_End = Frog_New;
        Frog_End.x += 1200;
        arc = 350f;
        duration = 1f;
        t = 0f;
        while (t < duration) {
            float normalizedTime = t / duration;
            float height = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 position = Vector2.Lerp(Frog_New, Frog_End, normalizedTime);
            position.y += height;
            player.rectTransform.anchoredPosition = position;
            t += Time.deltaTime;
            yield return null;
        }
        player.rectTransform.anchoredPosition = Frog_End;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator LoadInExitPlatform(Vector2 position) {
        GameObject exitObj = new GameObject("Exit_Platform");
        Image exitImage = exitObj.AddComponent<Image>();
        Color endColor = exitImage.color;
        Color startColor = exitImage.color;
        endColor.a = 0.47f;
        startColor.a = 0f;
        exitImage.color = startColor;

        exitImage.sprite = floor;
        exitImage.rectTransform.sizeDelta = new Vector2(floorWidth, floorHeight);
        exitImage.rectTransform.localScale = new Vector3(floorScale, 1, 1);
        exitImage.rectTransform.anchoredPosition = position;
        exitObj.transform.SetParent(canvas.transform, false);

        Color witch_endColor = witch.color;
        Color witch_startColor = new Color(255f, 255f, 255f, 0f);
        witch.color = witch_startColor;
        witch.gameObject.SetActive(true);

        //fade in the platform object
        float t = 0f;
        float duration = 0.5f;
        while (t < duration) {
            t += Time.deltaTime;
            exitImage.color = Color.Lerp(startColor, endColor, t / duration);
            witch.color = Color.Lerp(witch_startColor, witch_endColor, t / duration);
            yield return null;
        }

        exitImage.color = endColor;
        witch.color = witch_endColor;

        StartCoroutine(WitchRunAway());
    }
    
    private IEnumerator WitchRunAway() {
        Vector2 Witch_Original = witch.rectTransform.anchoredPosition;
        Vector2 Witch_New = Witch_Original;
        Witch_New.x += 1200f;

        float duration = 1f;
        float t = 0f;
        while (t < duration) {
            float normalizedTime = t / duration;
            Vector2 position = Vector2.Lerp(Witch_Original, Witch_New, normalizedTime);
            witch.rectTransform.anchoredPosition = position;
            t += Time.deltaTime;
            yield return null;
        }
        witch.rectTransform.anchoredPosition = Witch_New;
    }
}