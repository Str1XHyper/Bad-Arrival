using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    public GameObject boss;
    public float moveSpeed;
    public float lifeTime;
    private float currentLifeTime;
    public float sizeFactor;
    private float currentSize;
    private SphereCollider col;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        col = GetComponent<SphereCollider>();
        transform.position = boss.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(boss == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, boss.transform.position, moveSpeed * Time.deltaTime);
        }

        currentSize += Time.deltaTime * sizeFactor;
        currentLifeTime += Time.deltaTime;
        transform.localScale = new Vector3(currentSize, currentSize, currentSize);

        if (currentLifeTime > lifeTime)
        {
            Destroy(gameObject);
            currentLifeTime = 0;
            currentSize = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.ApplyDamage(Convert.ToInt32(player.MaxHP / 2));
        }
    }
}
