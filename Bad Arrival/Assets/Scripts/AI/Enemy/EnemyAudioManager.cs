using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource shootAudio;

    public void PlayShootAudio()
    {
        shootAudio.Play();
    }
}
