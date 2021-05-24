using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeAmount = 0.7f;
    [SerializeField] float decreaseFactor = 1.0f;

    Transform cameraTransform;
    float shakeDuration = 0f;
    Vector3 originalPos;

    void Awake()
    {
        cameraTransform = Camera.main.transform;
        if (cameraTransform == null)
        {
            Debug.LogError("CameraShake: Transform not found.");
        }
    }

    void OnEnable()
    {
        originalPos = cameraTransform.localPosition;
    }

    private void Start()
    {
        FindObjectOfType<Player>().OnPlayerDamaged += Player_OnPlayerDamaged;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            cameraTransform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            cameraTransform.localPosition = originalPos;
        }
    }

    void Player_OnPlayerDamaged(object sender, EventArgs e)
    {
        shakeDuration = .4f;
    }
}
