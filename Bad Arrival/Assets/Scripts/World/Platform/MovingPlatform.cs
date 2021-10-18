using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //public Transform startPosition;
    public Transform[] targetPositions;
    public GameObject platform;
    public float moveSpeed;
    private int currentTarget = 0;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeed / 100;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHandler();
    }

    public void MoveHandler()
    {
        if(Vector3.Distance(targetPositions[currentTarget].position, platform.transform.position) < 0.5f)
        {
            if((currentTarget + 1) < targetPositions.Length)
            {
                currentTarget++;
            }
            else
            {
                currentTarget = 0;
            }
        }

        platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPositions[currentTarget].position, moveSpeed);
    }
}
