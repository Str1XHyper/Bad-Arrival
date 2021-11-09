using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] private CapturePointObject CPSettings;
    [SerializeField] private ObjectiveReward reward;
    [SerializeField] private UnityEvent rewardEvent;
    [SerializeField] private UnityEvent startEvent;

    private bool eventStarted = false;

    private float timeLeftToCapInFrames;
    private bool playerInPoint;

    public float PercentageFinished => 100 - ((float)timeLeftToCapInFrames / ((float)CPSettings.TimeToCapInSeconds / Time.fixedDeltaTime) * 100);

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
        if (timeLeftToCapInFrames <= 0)
        {
            if(reward != null)
            {
                reward.CompleteObjective();
                
            }
            if(rewardEvent != null)
            {
                rewardEvent.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            if(eventStarted != true)
            {
                if (startEvent != null)
                {
                    startEvent.Invoke();
                    eventStarted = true;
                }

                playerInPoint = true;
                UIManager.instance.SetProgressBarTarget(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            playerInPoint = false;
            UIManager.instance.SetProgressBarTarget(null);
        }
    }
}
