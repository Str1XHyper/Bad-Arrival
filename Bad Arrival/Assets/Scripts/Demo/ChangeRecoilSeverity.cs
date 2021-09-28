using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class ChangeRecoilSeverity : MonoBehaviour
{
    CinemachineImpulseSource cinemachineImpulseSource;

    public TMP_Text txtRecoilAmount;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineImpulseSource = gameObject.GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            AddRecoil(0.25f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            AddRecoil(-0.25f);
        }
    }

    void AddRecoil(float amount)
    {
        if((cinemachineImpulseSource.m_ImpulseDefinition.m_AmplitudeGain + amount) >= 0)
        {
            cinemachineImpulseSource.m_ImpulseDefinition.m_AmplitudeGain += amount;
            string recoilText = cinemachineImpulseSource.m_ImpulseDefinition.m_AmplitudeGain.ToString() + "x";
            txtRecoilAmount.text = recoilText;
        }
    }
}
