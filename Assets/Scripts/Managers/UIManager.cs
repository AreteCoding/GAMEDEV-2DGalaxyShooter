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

    private void Start()
    {
        uiCanvas = FindObjectOfType<Canvas>();
        scoreText = uiCanvas.transform.Find("scoreText").GetComponent<TextMeshProUGUI>();

        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.OnScoreUpdated += ScoreManager_OnScoreUpdated;
    }
   
    void ScoreManager_OnScoreUpdated(object sender, EventArgs e)
    {
        scoreText.text = scoreManager.PlayerScore.ToString();
    }

}
