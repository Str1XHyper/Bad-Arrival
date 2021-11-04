using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float clampAngle = 90f;
    [SerializeField] private float horizontalSensitivity = 10f;
    [SerializeField] private float verticalSensitivity = 10f;

    private InputManager inputManager;
    private Vector3 startingRotation;

    protected override void Awake()
    {
        inputManager = InputManager.instance;
        startingRotation = transform.localRotation.eulerAngles;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if(inputManager == null)
                {
                    return;
                }
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * horizontalSensitivity * Time.deltaTime;
                startingRotation.y += deltaInput.y * verticalSensitivity * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }

    public void AddRecoil(Vector2 recoil)
    {
        startingRotation.x += recoil.x;
        startingRotation.y += recoil.y;
    }
}
