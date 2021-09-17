using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyTime;
    [SerializeField] private int damageAmount;
    private Rigidbody rb;
    private float elapsedTime;
    private Vector3 startPosition;
    [SerializeField] public Vector3 targetPosition;

    [Header("Trajectory Variables")]
    [SerializeField] private float maxHeight;
    [SerializeField] private float height;
    [SerializeField] private float startHeight;
    [SerializeField] private bool maxHeightReached = false;
    [SerializeField] private float timeToReachTarget = 5f;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startPosition = transform.position;
        startHeight = startPosition.y;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 center = (startPosition + targetPosition) * 0.5F;

        center -= new Vector3(0, 1, 0);

        Vector3 startPositionCenter = startPosition - center;
        Vector3 targetPositionCenter = targetPosition - center;

        float fracComplete = (Time.time - startTime) / timeToReachTarget;

        transform.position = Vector3.Slerp(startPositionCenter, targetPositionCenter, fracComplete);
        transform.position += center;

        if(Vector3.Distance(targetPosition, transform.position) < 0.5f)
        {
            Destroy(gameObject);
        }

        // Object Kill Timer
        /*
        elapsedTime += Time.deltaTime;
        if (elapsedTime > destroyTime)
        {
            Destroy(gameObject);
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ApplyDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
