using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Fight : MonoBehaviour
{
    public Canvas canvas;
    public Image player;

    public AudioSource music;

    private KeyCode left_control;
    private KeyCode right_control;

    private bool initialization_items_done = false;
    private float speed = 30f;

    void Start() {
        left_control = (KeyCode)PlayerPrefs.GetInt("Control2");
        right_control = (KeyCode)PlayerPrefs.GetInt("Control3");

        //load the "notes" (carrots) and other stuff
    }

    void Update() {
        if (PlayerPrefs.GetInt("Boss_fight_introduction_complete") == 1) {
            //initialize music and other items to start the fight
            if (initialization_items_done == false) {
                music.volume = PlayerPrefs.GetFloat("Volume");
                music.Play();
                initialization_items_done = true;
            }
            
            //player movement
            if (Input.GetKey(left_control)) {
                PlayerMovement(-1 * speed);
            }
            else if (Input.GetKey(right_control)) {
                PlayerMovement(speed);
            }

            //release notes, check hitboxes for a catch, etc
            
        }
    }

    //check if player is within the x axis boundaries of the canvas
    private bool CheckBoundaries() {
        float halfWidth = canvas.GetComponent<RectTransform>().rect.width / 2f;
        float playerPosition = player.GetComponent<RectTransform>().anchoredPosition.x;
        return (playerPosition > -halfWidth && playerPosition < halfWidth);
    }

    //move the player in the x axis direction at a speed based on the passed in value
    private void PlayerMovement(float speed) {
        if (!CheckBoundaries()) {
            return;
        }
        Vector2 currentPosition = player.GetComponent<RectTransform>().anchoredPosition;
        float halfWidth = (canvas.GetComponent<RectTransform>().rect.width / 2f) - 75f;
        float newXPosition = Mathf.Clamp(currentPosition.x + speed, -halfWidth, halfWidth);
        currentPosition.x = newXPosition;
        player.GetComponent<RectTransform>().anchoredPosition = currentPosition;
    }
}
