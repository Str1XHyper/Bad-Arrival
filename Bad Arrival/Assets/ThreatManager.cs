using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatManager : MonoBehaviour
{
    private static FMOD.Studio.EventInstance Music; //An event instance variable.
    // Start is called before the first frame update
    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Background");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Music, Camera.main.transform);
        Music.setVolume(0.2f);
        Music.start();
        Music.release();
    }

    public void SetThreatLevel(int threat)
    {
        Music.setParameterByName("Threat Level", threat);
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
