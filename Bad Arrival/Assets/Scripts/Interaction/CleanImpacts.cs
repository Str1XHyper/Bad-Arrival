using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanImpacts : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Remove", 3);
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}
