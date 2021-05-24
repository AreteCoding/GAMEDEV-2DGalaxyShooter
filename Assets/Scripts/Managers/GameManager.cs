using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Player player;
    bool isPlayerDead = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.OnPlayerDied += Player_OnPlayerDeath;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!isPlayerDead) { return; }

        if(Input.GetKeyDown(KeyCode.R))
        {
            isPlayerDead = false;
            SceneManager.LoadScene("GameScene");
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenuScene");
        }

    }

    void Player_OnPlayerDeath(object sender, EventArgs e)
    {
        if (player.PlayerLives == 0)
        {
            isPlayerDead = true;
        }
    }
}
