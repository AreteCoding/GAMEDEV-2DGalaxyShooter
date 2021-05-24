using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHealth : MonoBehaviour, IPowerup
{
    public void SetPlayer(Player player)
    {
        player.AddHealth(1);
    }
}
