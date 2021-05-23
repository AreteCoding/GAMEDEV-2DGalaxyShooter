using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioClip laserClip;

    AudioSource audioSource;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.OnPlayerFired += Player_OnPlayerFired;

        audioSource = GetComponent<AudioSource>();
    }

    void Player_OnPlayerFired(object sender, EventArgs e)
    {
        audioSource.clip = laserClip;
        audioSource.Play();
    }
}
