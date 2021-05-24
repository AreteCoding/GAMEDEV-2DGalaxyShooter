using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    Canvas uiCanvas;

    TextMeshProUGUI scoreText;

    ScoreManager scoreManager;
    LivesManager livesManager;


    private void Start()
    {
        uiCanvas = FindObjectOfType<Canvas>();
        scoreText = uiCanvas.transform.Find("scoreText").GetComponent<TextMeshProUGUI>();

        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.OnScoreUpdated += ScoreManager_OnScoreUpdated;
    }

    public void SetShield(PowerupShield shield)
    {
        shield.OnShieldDamaged += PowerupShield_OnShieldDamaged;
    }

    void ScoreManager_OnScoreUpdated(object sender, EventArgs e)
    {
        scoreText.text = scoreManager.PlayerScore.ToString();
    }

    void PowerupShield_OnShieldDamaged(object sender, EventArgs e)
    {
        PowerupShield shield = sender as PowerupShield;

        int currentShield = shield.CurrentShieldAmount;
    }

}
