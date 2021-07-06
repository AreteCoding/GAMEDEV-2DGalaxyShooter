using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpeed : MonoBehaviour, IPowerup
{
    Player player;
    [SerializeField] float speedAmount;

    [SerializeField] float powerupDuration;

    public void SetPlayer(Player player)
    {
        this.player = player;

        player.AddMoveSpeedMultiplier(speedAmount);
        StartCoroutine(PowerdownRoutine());
    }

    IEnumerator PowerdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        player.AddMoveSpeedMultiplier(-speedAmount);
        Destroy(gameObject);
    }
}
