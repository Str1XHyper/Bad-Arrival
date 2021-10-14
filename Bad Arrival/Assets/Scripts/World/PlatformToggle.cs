using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToggle : MonoBehaviour
{
    private BoxCollider boxCollider;
    [SerializeField]private float enabledTime;
    [SerializeField] private float disabledTime;
    [SerializeField] private float dissolveTime;
    private float timeSinceLastStateChange;
    [SerializeField] private PlatformStates platformState;
    private float dissolveFactor;
    private Renderer renderer;

    public enum PlatformStates
    {
        Dissolving,
        Materializing,
        Enabled,
        disabled
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        dissolveFactor = 1 / dissolveTime;
        platformState = PlatformStates.Enabled;
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();
    }

    private void StateHandler()
    {
        switch (platformState)
        {
            case PlatformStates.Dissolving:
                Dissolving();
                break;
            case PlatformStates.Materializing:
                Materializing();
                break;
            case PlatformStates.Enabled:
                Enabled();
                break;
            case PlatformStates.disabled:
                Disabled();
                break;
            default:
                break;
        }
    }

 
    private void Dissolving()
    {
        timeSinceLastStateChange += Time.deltaTime;
        float dissolveAmount = Mathf.Clamp(dissolveFactor * timeSinceLastStateChange, 0f, 1f);
        renderer.material.SetFloat("_Dissolve", dissolveAmount);

        if (timeSinceLastStateChange >= dissolveTime)
        {
            timeSinceLastStateChange = 0;
            platformState = PlatformStates.disabled;
            boxCollider.enabled = false;
        }
    }

    private void Materializing()
    {
        timeSinceLastStateChange += Time.deltaTime;
        float dissolveAmount = Mathf.Clamp(1 - (dissolveFactor * timeSinceLastStateChange), 0f, 1f);
        renderer.material.SetFloat("_Dissolve", dissolveAmount);

        if (timeSinceLastStateChange >= dissolveTime)
        {
            timeSinceLastStateChange = 0;
            platformState = PlatformStates.Enabled;
        }
    }

    private void Enabled()
    {
        timeSinceLastStateChange += Time.deltaTime;
        if (timeSinceLastStateChange >= enabledTime)
        {
            timeSinceLastStateChange = 0;
            platformState = PlatformStates.Dissolving;
        }
    }

    private void Disabled()
    {
        timeSinceLastStateChange += Time.deltaTime;
        if (timeSinceLastStateChange >= disabledTime)
        {
            timeSinceLastStateChange = 0;
            platformState = PlatformStates.Materializing;
            boxCollider.enabled = true;
        }
    }
}
