using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThreatTest : MonoBehaviour
{
    public UnityEvent setThreatHigh;
    public UnityEvent setThreatLow;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(setThreatHigh != null)
            {
                setThreatHigh.Invoke();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (setThreatLow != null)
            {
                setThreatLow.Invoke();
            }
        }
    }
}
