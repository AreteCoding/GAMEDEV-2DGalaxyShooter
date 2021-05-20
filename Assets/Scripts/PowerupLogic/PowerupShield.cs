using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupShield : MonoBehaviour, IPowerup
{

    [SerializeField] GameObject pfShield;
    [SerializeField] float powerupDuration;
    Player player;

    public void SetPlayer(Player player)
    {
        this.player = player;

        GameObject newShield = Instantiate(pfShield);
        newShield.transform.parent = player.transform;
        newShield.transform.position = player.transform.position;

        StartCoroutine(PowerdownRoutine());
    }

    IEnumerator PowerdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        Destroy(gameObject);
    }
}
