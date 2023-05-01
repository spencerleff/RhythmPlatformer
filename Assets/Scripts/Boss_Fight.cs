using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Boss_Fight : MonoBehaviour
{
    public Canvas canvas;
    public Image player;
    public Sprite one_carrot;
    public Sprite two_carrot;
    public Sprite three_carrot;
    public Image witch;
    public Sprite carrot;
    public RawImage background;
    public RawImage bg1;
    public RawImage bg2;
    public AudioSource music;
    public GameObject gameOverMenu;
    public AudioSource gameOverSound;
    public AudioSource healSound;
    public AudioSource hurtSound;
    public Sprite heart_full;
    public Sprite heart_half;
    public Sprite heart_empty;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public TextMeshProUGUI completion;
    public TextMeshProUGUI combo;

    private List<GameObject> notesList = new List<GameObject>();
    static float noteSize = 175f;
    private KeyCode left_control;
    private KeyCode right_control;
    private Color textColor = new Color(1f, 1f, 1f, 0.70588f);
    private bool initialization_items_done = false;
    private float player_speed = 19f;
    private float player_hitbox = 100f;
    private float note_speed = 1300f;
    private bool gameOver = false;
    private int note_counter = 0;
    private int total_notes = 80;
    private float note_difficulty_modifier = 1.25f;
    private bool isLevelCompleted = false;

    public class NoteController : MonoBehaviour
    {
        public bool shouldMove { get; set; }
    }

    List<(float x, float delay)> notes = new List<(float, float)> {
        (0f, 0f),
        (-250f, 0.5f),
        (-500f, 0.5f),
        (-750f, 0.5f),
        (-1000f, 0.5f),
        (-750f, 0.5f),
        (-500f, 0.5f),
        (-250f, 0.5f),

        (0f, 1f),
        (-1000f, 1f),
        (1000f, 2f),
        (-750f, 1.75f),
        (750f, 1.5f),
        (-500f, 1.25f),
        (500f, 1f),
        (-250f, 0.75f),
        (250f, 0.5f),

        (0f, 1f),
        (250f, 0.5f),
        (500f, 0.5f),
        (750f, 0.5f),
        (1000f, 0.5f),
        (750f, 0.5f),
        (500f, 0.5f),
        (250f, 0.5f),

        (0f, 1f),
        (1000f, 1f),
        (-1000f, 2f),
        (750f, 1.75f),
        (-750f, 1.5f),
        (500f, 1.25f),
        (-500f, 1f),
        (250f, 0.75f),
        (-250f, 0.5f),

        (0f, 1f),
        (-200f, 0.3f),
        (-400f, 0.3f),
        (-600f, 0.3f),
        (-800f, 0.3f),
        (-1000f, 0.3f),
        (-800f, 0.3f),
        (-600f, 0.3f),
        (-400f, 0.3f),
        (-200f, 0.3f),

        (0f, 1f),
        (500f, 0.4f),
        (0f, 0.4f),
        (1000f, 0.8f),
        
        (0f, 0.8f),
        (200f, 0.3f),
        (400f, 0.3f),
        (600f, 0.3f),
        (800f, 0.3f),
        (1000f, 0.3f),
        (800f, 0.3f),
        (600f, 0.3f),
        (400f, 0.3f),
        (200f, 0.3f),

        (0f, 1f),
        (-500f, 0.4f),
        (0f, 0.4f),
        (-1000f, 0.8f),

        (0f, 0.8f),
        (-300f, 0.3f),
        (-150f, 0.15f),
        (-600f, 0.45f),
        (-450f, 0.15f),
        (450f, 0.9f),
        (600f, 0.15f),
        (150f, 0.45f),
        (300f, 0.15f),
        
        (0f, 0.3f),
        (300f, 0.3f),
        (150f, 0.15f),
        (600f, 0.45f),
        (450f, 0.15f),
        (-450f, 0.9f),
        (-600f, 0.15f),
        (-150f, 0.45f),
        (-300f, 0.15f),

        (0, 0.5f)
    };

    void Start() {
        left_control = (KeyCode)PlayerPrefs.GetInt("Control2");
        right_control = (KeyCode)PlayerPrefs.GetInt("Control3");

        if (PlayerPrefs.GetInt("Extreme_Active") == 1) {
            player_speed = 26f;
            note_speed = 2100f;
            note_difficulty_modifier = 0.75f;
            music.pitch = 1.05f;
        }
    }

    void Update() {
        if (PlayerPrefs.GetInt("PauseActive") == 0 && !gameOver) {
            //move background
            bg1.uvRect = new Rect(bg1.uvRect.x + 0.0000075f, bg1.uvRect.y + 0.005f * Time.deltaTime, bg1.uvRect.width, bg1.uvRect.height);
            bg2.uvRect = new Rect(bg2.uvRect.x - 0.0000025f, bg2.uvRect.y + 0.0025f * Time.deltaTime, bg2.uvRect.width, bg2.uvRect.height);

            if (PlayerPrefs.GetInt("Boss_fight_introduction_complete") == 1) {
                //initialize music and other items to start the fight
                if (initialization_items_done == false) {
                    PlayerPrefs.SetInt("CountdownActive", 0);
                    StartCoroutine(DropNotes());
                }
                else {
                    //check for player win
                    if (note_counter == total_notes && isLevelCompleted == false) {
                        player.sprite = three_carrot;
                        StopAllCoroutines();
                        StartCoroutine(LevelCompleted());
                        isLevelCompleted = true;
                    }

                    //player movement
                    if (Input.GetKey(left_control)) {
                        PlayerMovement(-1 * player_speed);
                    }
                    else if (Input.GetKey(right_control)) {
                        PlayerMovement(player_speed);
                    }

                    //move notes
                    for (int i = 0; i < notesList.Count; i++) {
                        if (notesList[i].GetComponent<NoteController>().shouldMove) {
                            MoveNote(notesList[i]);
                        }
                    }
                } 
            }
        }
    }

    //start dropping notes
    private IEnumerator DropNotes() {
        initialization_items_done = true;
        music.volume = PlayerPrefs.GetFloat("Volume");
        music.Play();

        foreach (var note_variables in notes) {
            yield return StartCoroutine(CN(note_variables.x, (note_variables.delay * note_difficulty_modifier)));
        }
    }

    //Create Note
    private IEnumerator CN(float x, float duration)
    {
        float t = 0f;
        Vector2 startPos = witch.rectTransform.anchoredPosition;
        while (t < duration) {

            //don't move witch while the pause menu is open
            while (PlayerPrefs.GetInt("PauseActive") == 1) {
                yield return null;
            }

            t += Time.deltaTime;
            float normalizedTime = t / duration;
            Vector2 position = Vector2.Lerp(startPos, new Vector2(x, startPos.y), normalizedTime);
            witch.rectTransform.anchoredPosition = position;
            yield return null;
        }
        witch.rectTransform.anchoredPosition = new Vector2(x, startPos.y);

        GenerateNotesAtPos(witch.rectTransform.anchoredPosition);
    }

    //Generate the note
    private void GenerateNotesAtPos(Vector2 position)
    {
        GameObject noteObj = new GameObject("Note");
        Image noteImage = noteObj.AddComponent<Image>();
        noteImage.sprite = carrot;
        noteImage.rectTransform.sizeDelta = new Vector2(noteSize, noteSize);
        noteImage.rectTransform.anchoredPosition = position;
        noteObj.transform.SetParent(canvas.transform, false);
        notesList.Add(noteObj);

        noteObj.AddComponent<NoteController>().shouldMove = true;
    }

    //make the notes begin to fall at the rate of the 'note_speed' var
    private void MoveNote(GameObject noteObj)
    {
        Image noteImage = noteObj.GetComponent<Image>();
        noteImage.rectTransform.anchoredPosition += new Vector2(0, -note_speed * Time.deltaTime);

        CheckForNoteHit(noteImage);
    }

    //check for a hit or miss
    private void CheckForNoteHit(Image noteImage) {
        //in y range of a possible hit
        if (noteImage.rectTransform.anchoredPosition.y < player.rectTransform.anchoredPosition.y + player_hitbox) {
            
            // hit
            if ((Mathf.Abs(noteImage.rectTransform.anchoredPosition.x - player.rectTransform.anchoredPosition.x) < player_hitbox)) {
                notesList.Remove(noteImage.gameObject);
                Destroy(noteImage.gameObject);

                note_counter += 1;
                UpdateCompletionPercentage();
                
                combo.text = (int.Parse(combo.text) + 1).ToString();
                //combo > 60
                if (int.Parse(combo.text) > 60 && int.Parse(combo.text) % 10 == 0) {
                    GainHealth();
                }
                //combo - 50
                else if (int.Parse(combo.text) == 50) {
                    combo.color = new Color32(255, 0, 0, 255);
                    GainHealth();
                }
                //combo - 40
                else if (int.Parse(combo.text) == 40) {
                    combo.color = new Color32(255, 150, 0, 255);
                    GainHealth();
                }
                //combo - 30
                else if (int.Parse(combo.text) == 30) {
                    combo.color = new Color32(255, 255, 0, 255);
                    GainHealth();
                }
                //combo - 20
                else if (int.Parse(combo.text) == 20) {
                    combo.color = new Color32(0, 255, 0, 255);
                    GainHealth();
                }
                //combo - 10
                else if (int.Parse(combo.text) == 10) {
                    combo.color = new Color32(0, 150, 0, 255);
                    GainHealth();
                }
            }

            //miss
            else if (noteImage.rectTransform.anchoredPosition.y < player.rectTransform.anchoredPosition.y - 70f) {
                notesList.Remove(noteImage.gameObject);
                Destroy(noteImage.gameObject);

                note_counter += 1;
                UpdateCompletionPercentage();

                LoseHealth();
                combo.text = "0";
                combo.color = textColor;
            }
        }
    }
    
    //update completion percentage
    private void UpdateCompletionPercentage() {
        float percentage = note_counter * 100f / total_notes;
        if (percentage > 99) {
            percentage = 100;
        }
        else if (percentage >= 60 && percentage <= 70) {
            player.sprite = two_carrot;
        }
        else if (percentage >= 30 && percentage <= 40) {
            player.sprite = one_carrot;
        }
        else if (percentage < 0) {
            percentage = 0;
        }
        completion.text = $"{percentage:F0}%";
    }

    //check if player is within the x axis boundaries of the canvas
    private bool CheckBoundaries() {
        float halfWidth = canvas.GetComponent<RectTransform>().rect.width / 2f;
        float playerPosition = player.GetComponent<RectTransform>().anchoredPosition.x;
        return (playerPosition > -halfWidth && playerPosition < halfWidth);
    }

    //move the player in the x axis direction at a speed based on the passed in value
    private void PlayerMovement(float movement_speed) {
        if (!CheckBoundaries()) {
            return;
        }
        Vector2 currentPosition = player.GetComponent<RectTransform>().anchoredPosition;
        float halfWidth = (canvas.GetComponent<RectTransform>().rect.width / 2f) - 75f;
        float newXPosition = Mathf.Clamp(currentPosition.x + movement_speed, -halfWidth, halfWidth);
        currentPosition.x = newXPosition;
        player.GetComponent<RectTransform>().anchoredPosition = currentPosition;
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
        music.Stop();
        StopAllCoroutines();
        gameOverMenu.SetActive(true);
        gameOverSound.Play();
    }

    private IEnumerator LevelCompleted() {
        float startVolume = music.volume;
        float music_fade_duration = 1f;
        float timePassed = 0f;

        while (music.volume > 0)
        {
            timePassed += Time.deltaTime;
            float fractionCompleted = timePassed / music_fade_duration;
            music.volume = Mathf.Lerp(startVolume, 0, fractionCompleted);
            yield return null;
        }

        music.Stop();
        StartCoroutine(WitchFleeAnimation());
    }

    private IEnumerator WitchFleeAnimation() {
        Vector2 Witch_Original = witch.rectTransform.anchoredPosition;
        Vector2 Witch_New = new Vector2(1500f, 600f);
        float arc = 100f;

        float duration = 1.25f;
        float t = 0f;
        while (t < duration) {
            float normalizedTime = t / duration;
            float height = Mathf.Sin(normalizedTime * Mathf.PI) * arc;
            Vector2 position = Vector2.Lerp(Witch_Original, Witch_New, normalizedTime);
            position.y += height;
            witch.rectTransform.anchoredPosition = position;
            t += Time.deltaTime;
            yield return null;
        }
        witch.rectTransform.anchoredPosition = Witch_New;

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(FrogGoesHome());
    }

    private IEnumerator FrogGoesHome() {
        Vector2 Frog_Original = player.rectTransform.anchoredPosition;
        Vector2 Frog_New = Frog_Original;
        Frog_New.x = -2000;
        float arc = 500f;

        float duration = 1.5f;
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
