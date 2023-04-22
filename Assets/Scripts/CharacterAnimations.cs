using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimations : MonoBehaviour
{
    public RawImage background;
    public RawImage bg1;
    public RawImage bg2;
    public Image player;
    
    private RectTransform background_rt;
    private RectTransform player_rt;
    
    private float moveSpeed = 6f;
    private float playerAngle = 0f;
    private float spinSpeed = -50f;

    private bool isLoadInAnimationFinished = false;
    
    void Start()
    {
        background_rt = background.GetComponent<RectTransform>();
        player_rt = player.GetComponent<RectTransform>();

        playerAngle = Random.Range(0f, 360f);

        StartCoroutine(loadInAnimation());
    }

    void Update()
    {
        if (isLoadInAnimationFinished) {
            MovePlayer();
            RotatePlayer();
        }

        //parallax
        bg1.uvRect = new Rect(bg1.uvRect.x + 0.00010f, bg1.uvRect.y + 0.04f * Time.deltaTime, bg1.uvRect.width, bg1.uvRect.height);
        bg2.uvRect = new Rect(bg2.uvRect.x - 0.00005f, bg2.uvRect.y + 0.02f * Time.deltaTime, bg2.uvRect.width, bg2.uvRect.height);
    }

    private IEnumerator loadInAnimation() 
    {
        player.color = new Color(player.color.r, player.color.g, player.color.b, 0f);

        //load in player
        Vector2 startPosition = new Vector2(Random.Range(background_rt.rect.xMin + 200f, background_rt.rect.xMax - 200f), Random.Range(background_rt.rect.yMin + 200f, background_rt.rect.yMax - 200f));
        player_rt.anchoredPosition = startPosition;

        float fadeInDuration = 2f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            player.color = new Color(player.color.r, player.color.g, player.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.color = new Color(player.color.r, player.color.g, player.color.b, 1f);

        isLoadInAnimationFinished = true;
    }

    void MovePlayer()
    {
        // Move the player in the current direction
        Vector2 direction = Quaternion.Euler(0f, 0f, playerAngle) * Vector2.right;
        player_rt.anchoredPosition += direction * moveSpeed;

        float xMin = background_rt.rect.xMin + player_rt.rect.width / 2f;
        float xMax = background_rt.rect.xMax - player_rt.rect.width / 2f;
        float yMin = background_rt.rect.yMin + player_rt.rect.height / 2f;
        float yMax = background_rt.rect.yMax - player_rt.rect.height / 2f;
        Vector2 playerPos = player_rt.anchoredPosition;
        if (playerPos.x < xMin)
        {
            playerPos.x = xMin;
            playerAngle = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;
        }
        else if (playerPos.x > xMax)
        {
            playerPos.x = xMax;
            playerAngle = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;
        }
        if (playerPos.y < yMin)
        {
            playerPos.y = yMin;
            playerAngle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;
        }
        else if (playerPos.y > yMax)
        {
            playerPos.y = yMax;
            playerAngle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;
        }
        player_rt.anchoredPosition = playerPos;
    }

    void RotatePlayer()
    {
        float newAngle = player_rt.localEulerAngles.z + spinSpeed * Time.deltaTime;
        player_rt.localEulerAngles = new Vector3(0f, 0f, newAngle);
    }
}