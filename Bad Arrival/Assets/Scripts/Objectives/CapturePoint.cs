using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] private CapturePointObject CPSettings;

    private float timeLeftToCapInFrames;
    private bool playerInPoint;

    void Start()
    {
        timeLeftToCapInFrames = CPSettings.TimeToCapInSeconds / Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        if (playerInPoint)
        {
            timeLeftToCapInFrames--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            playerInPoint = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            playerInPoint = false;
        }
    }
}
