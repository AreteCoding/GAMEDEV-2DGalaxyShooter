using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ThrusterUI : MonoBehaviour
{
    [SerializeField] Image thrusterImage;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
         player.OnThrustersUsed += Player_OnThrustersUsed;
    }
    
    void Player_OnThrustersUsed(object sender, EventArgs e)
    {
        thrusterImage.fillAmount = Mathf.Clamp(player.GetThrusterAmount(), 0, 1);
    }
}
