using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTripleShot : MonoBehaviour, IPowerup
{

    Player player;
    [SerializeField] GameObject powerupProjectile;
    GameObject replacedProjectile;

    [SerializeField] float powerupDuration;

    public void SetPlayer(Player player)
    {
        this.player = player;
        replacedProjectile = player.CurrentProjectile;
        player.SetProjectile(powerupProjectile);

        StartCoroutine(PowerdownRoutine());
    }

    IEnumerator PowerdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        player.SetProjectile(replacedProjectile);
        Destroy(gameObject);
    }
}
