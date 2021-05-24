using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupAmmo : MonoBehaviour, IPowerup
{
    public void SetPlayer(Player player)
    {
        player.AddAmmo(player.ProjectileCapacity);
    }
}
