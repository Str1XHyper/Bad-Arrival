using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    public GameObject player;
    private Vector3 startPosition;
    private CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = player.transform.position;
        characterController = player.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        characterController.enabled = false;
        player.transform.position = startPosition;
        characterController.enabled = true;
    }
}
