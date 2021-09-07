using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    private float elapsedTime;
    private float timeBetweenDirectionSwaps;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
           Debug.Log(Time.time);
        //elapsedTime += Time.deltaTime;

        if (Time.deltaTime > 5)
        {
            
        }
        transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    }
}
