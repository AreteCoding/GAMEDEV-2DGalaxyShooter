using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShieldUI : MonoBehaviour
{
    Image[] shieldImages;
    PowerupShield activeShield;

    private void Awake()
    {
        shieldImages = GetComponentsInChildren<Image>();

        foreach (Image image in shieldImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void SetShield(PowerupShield shield)
    {
        activeShield = shield;
        shield.OnShieldDamaged += Shield_OnShieldDamaged;

        foreach(Image image in shieldImages)
        {
            image.gameObject.SetActive(true);
        }
    }

    void Shield_OnShieldDamaged(object sender, EventArgs e)
    {
        if(activeShield != null)
        {
            shieldImages[activeShield.CurrentShieldAmount].gameObject.SetActive(false);
        }
    }
}
