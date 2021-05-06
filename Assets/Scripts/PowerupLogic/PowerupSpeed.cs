using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpeed : MonoBehaviour, IPowerup
{
    Player player;
    [SerializeField] float powerupMoveSpeed;
    [SerializeField] float speedMultiplier;
    float addedSpeed;

    [SerializeField] float powerupDuration;

    public void SetPlayer(Player player)
    {
        this.player = player;

        addedSpeed = player.MoveSpeed * speedMultiplier;
        player.AddMoveSpeed(addedSpeed);
        StartCoroutine(PowerdownRoutine());
    }

    IEnumerator PowerdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        player.AddMoveSpeed(-addedSpeed);
        Destroy(gameObject);
    }
}
