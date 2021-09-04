using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private GameObject PhysicalBulletImpact;
    [SerializeField] private GunObject heldGun;


    private bool isFiring;
    private bool lastFirestate;
    public int cooldown;

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (heldGun.FireType == FireTypes.Full_Auto)
        {
            if (Input.GetKey(KeyCode.Mouse0) && cooldown <= 0)
            {
                Shoot(heldGun);
            }
        } 
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown <= 0)
            {
                Shoot(heldGun);
            }
        }


        cooldown--;
    }

    private void Shoot(GunObject heldGun)
    {
        Transform transform = Camera.main.transform;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            Instantiate(PhysicalBulletImpact, hit.point, transform.rotation);
            Debug.Log(transform.forward);
        }
        cooldown = Mathf.RoundToInt(60f / heldGun.RoundsPerMinute / Time.fixedDeltaTime);
    }
}