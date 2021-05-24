using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LivesManager : MonoBehaviour
{
    [SerializeField] Sprite[] livesSprites;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI restartGameText;

    Image livesImage;
    Player player;

    private void Start()
    {
        Canvas uiCanvas = FindObjectOfType<Canvas>();
        livesImage = uiCanvas.transform.Find("LivesVisual").GetComponent<Image>();
        gameOverText.gameObject.SetActive(false);
        restartGameText.gameObject.SetActive(false);

        player  = FindObjectOfType<Player>();
        player.OnPlayerDamaged += Player_OnPlayerDamaged;
        player.OnPlayerHealed += Player_OnPlayerDamaged;

        livesImage.sprite = livesSprites[player.PlayerLives];
    }

    void Player_OnPlayerDamaged(object sender, EventArgs e)
    {

        livesImage.sprite = livesSprites[player.PlayerLives];
        if(player.PlayerLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        gameOverText.gameObject.SetActive(true);
        restartGameText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlashRoutine());
    }

    IEnumerator GameOverFlashRoutine()
    {
        while(true)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(.4f);
            gameOverText.text = "";
            yield return new WaitForSeconds(.2f);
        }
    }
}
