using UnityEngine;

public class ResetBarrier : MonoBehaviour
{
    public Transform player;
    public Transform resetPoint;
    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.position = resetPoint.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
