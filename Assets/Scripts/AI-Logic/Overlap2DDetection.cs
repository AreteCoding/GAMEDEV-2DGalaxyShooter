using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Overlap2DDetection : MonoBehaviour
{
    public LayerMask detectionLayerMask;

    public UnityEvent<GameObject> OnObjectDetected;
    public GameObject DetectedObject { get; private set; }

    [Header("Box Overlap Parameters")]
    [SerializeField] Transform detectionOrigin;
    public Vector2 detectionSize = Vector2.one;
    public Vector2 detectionOffset = Vector2.zero;
    public float detectionDelay = .25f;

    [Header("Gizmo paramaters")]
    public bool showGizmos = false;
    public Color detectionColorActive = Color.green;
    public Color detectionColorIdle = Color.red;

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    private void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectionOrigin.position + detectionOffset, detectionSize, 0, detectionLayerMask);

        if(collider != null)
        {
            DetectedObject = collider.gameObject;
            OnObjectDetected?.Invoke(collider.gameObject);
        }
        else
        {
            DetectedObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        if(showGizmos && detectionOrigin != null)
        {
            Gizmos.color = detectionColorIdle;

            if(DetectedObject)
            {
                Gizmos.color = detectionColorActive;

            }

            Gizmos.DrawCube((Vector2)detectionOrigin.position + detectionOffset, detectionSize);
        }
    }
}
