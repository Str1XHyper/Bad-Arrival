using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyTime;
    private Rigidbody rb;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        rb.velocity = transform.forward * speed;
        if(elapsedTime > destroyTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO
            //Deal Damage
        }
        else if (!other.CompareTag("Enemy"))
        {
            //Destroy(gameObject);
        }
    }
}
