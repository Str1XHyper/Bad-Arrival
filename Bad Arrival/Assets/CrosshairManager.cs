using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    #region Singleton
    public static CrosshairManager instance;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }
    #endregion

    private void Start()
    {
        currentCrosshair = defaultCrosshair;
    }

    [SerializeField] private Texture2D defaultCrosshair;
    [Range(0.1f, 2)]
    [SerializeField] private float crosshairScale = 0.5f;
    private Texture2D currentCrosshair;
    private void OnGUI()
    {
        float xMin = (Screen.width / 2) - (currentCrosshair.width * crosshairScale / 2);
        float yMin = (Screen.height / 2) - (currentCrosshair.height * crosshairScale / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, currentCrosshair.width * crosshairScale, currentCrosshair.height * crosshairScale), currentCrosshair);
    }

    public void SetCrosshair(Texture2D crosshair)
    {
        if(crosshair != null)
        {
            currentCrosshair = crosshair;
        }
        else
        {
            currentCrosshair = defaultCrosshair;
        }
    }
}
