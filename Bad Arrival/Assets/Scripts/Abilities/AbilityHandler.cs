using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] abilities;
    private Transform player;
    [SerializeField] private Transform projectileExit;
    Quaternion newRotation;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance.transform;
    }

    public void Attack()
    {
        newRotation = Quaternion.LookRotation((player.position - projectileExit.position).normalized);
        Instantiate(abilities[0], projectileExit.position, newRotation);
    }

}
