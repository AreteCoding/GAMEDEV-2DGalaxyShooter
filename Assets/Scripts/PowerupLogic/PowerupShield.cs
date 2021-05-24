using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerupShield : MonoBehaviour, IPowerup, IDamage
{
    public EventHandler OnShieldDamaged;

    [SerializeField] GameObject pfShield;
    [SerializeField] int shieldAmountMax = 3;
    
    int currentShieldAmount;
    public int CurrentShieldAmount => currentShieldAmount;

    Player player;

    public void SetPlayer(Player player)
    {
        this.player = player;
        gameObject.transform.parent = player.transform;
        gameObject.transform.position = player.transform.position;

        GameObject newShield = Instantiate(pfShield);
        newShield.transform.parent = gameObject.transform;
        newShield.transform.position = gameObject.transform.position;

        currentShieldAmount = shieldAmountMax;

        SetupUI();
    }

    void SetupUI()
    {
        ShieldUI shieldUI = FindObjectOfType<ShieldUI>();

        if(shieldUI != null)
        {
            shieldUI.SetShield(this);
        }
    }
    public void Damage()
    {
        currentShieldAmount--;
        OnShieldDamaged?.Invoke(this, EventArgs.Empty);

        if (currentShieldAmount < 1)
        {
            Destroy(this.gameObject);
        }
    }
 
}
