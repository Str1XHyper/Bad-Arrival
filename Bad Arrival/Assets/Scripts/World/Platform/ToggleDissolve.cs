using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDissolve : MonoBehaviour
{
    private MeshCollider collider;
    [SerializeField]private float enabledTime;
    [SerializeField] private float disabledTime;
    [SerializeField] private float dissolveTime;
    [SerializeField] private float startDelay;
    [SerializeField] private float dissolveScale;
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
        renderer.material.SetFloat("_DissolveScale", dissolveScale);
        dissolveFactor = 1 / dissolveTime;
        platformState = PlatformStates.Enabled;
        collider = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();
    }

    private void StateHandler()
    {
        if(Time.timeSinceLevelLoad > startDelay)
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
            collider.enabled = false;
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
            collider.enabled = true;
        }
    }
}
