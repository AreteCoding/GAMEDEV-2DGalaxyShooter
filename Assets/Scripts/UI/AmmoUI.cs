using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    TextMeshProUGUI ammoText;
    Player player;


    private void Awake()
    {
        ammoText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();

        player.OnPlayerFired += Player_OnPlayerFired;
        ammoText.text = player.ProjectileCount.ToString();
    }

    void Player_OnPlayerFired(object sender, EventArgs e)
    {
        ammoText.text = player.ProjectileCount.ToString();
    }
}
