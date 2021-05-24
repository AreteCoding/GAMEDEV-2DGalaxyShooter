using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpeed : MonoBehaviour, IPowerup
{
    Player player;
    [SerializeField] float powerupMoveSpeed;
    [SerializeField] float speedMultiplier;

    [SerializeField] float powerupDuration;

    public void SetPlayer(Player player)
    {
        this.player = player;

        player.AddMoveSpeedMultiplier(speedMultiplier);
        StartCoroutine(PowerdownRoutine());
    }

    IEnumerator PowerdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        player.AddMoveSpeedMultiplier(-speedMultiplier);
        Destroy(gameObject);
    }
}
