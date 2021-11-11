using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private GameObject player;
    private Vector3 startPosition;
    private CharacterController characterController;

    public List<GameObject> spawnPoints;
    private int currentSpawnPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance.gameObject;
        startPosition = player.transform.position;
        characterController = player.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        characterController.enabled = false;
        player.transform.position = spawnPoints[currentSpawnPointIndex].transform.position;
        characterController.enabled = true;
    }

    public void NextSpawnPoint()
    {
        if (spawnPoints.Count > (currentSpawnPointIndex + 1))
        {
            currentSpawnPointIndex++;
        }
    }
}
