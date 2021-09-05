using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        if(instance == null || instance != this)
        {
            instance = this;
        }
        mouseLook = playerController.GetComponent<MouseLook>();
    }
    #endregion

    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject inventoryHolder;
    [SerializeField] private GameObject interactMessage;
    private MouseLook mouseLook;

    private void Start()
    {
        //mouseLook = GetComponent<MouseLook>();
    }

    public void OpenInventory()
    {
        Cursor.lockState = CursorLockMode.None;
        mouseLook.SetLookState(false);
        inventoryHolder.SetActive(true);
    }

    public void CloseInventory()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.SetLookState(true);
        inventoryHolder.SetActive(false);
    }

    public void CanInteract()
    {
        interactMessage.SetActive(true);
    }
    public void CanNotInteract()
    {
        interactMessage.SetActive(false);
    }
}
