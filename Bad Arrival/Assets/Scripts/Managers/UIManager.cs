using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
        mouseLook = playerController.GetComponent<MouseLook>();
    }
    #endregion

    [Header("Visuals")]
    public Sprite energyIcon;
    public Sprite physicalIcon;
    public Sprite UIMask;

    [Range(0.1f, 2)]
    [SerializeField] private float hitmarkerScale = 0.5f;
    public Texture2D Hitmarker;
    private bool drawHitmarker = false;

    [Header("Colors")]
    public Color CommonColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public Color UncommonColor = new Color(0.5f, 0.7f, 0.5f, 0.5f);
    public Color RareColor = new Color(0.5f, 0.6f, 0.7f, 0.5f);
    public Color EpicColor = new Color(0.6f, 0.5f, 0.7f, 0.5f);
    public Color LegendaryColor = new Color(0.7f, 0.6f, 0.5f, 0.5f);

    [Header("Player info")]
    public PlayerInfo playerInfo;

    [Space]
    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject inventoryHolder;
    [SerializeField] private GameObject interactMessage;
    [SerializeField] private GameObject ActiveSlot1;
    [SerializeField] private GameObject ActiveSlot2;
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

    public void UpdateActiveSlot1(Item item, ItemObject itemObject)
    {
        var slotInfo = ActiveSlot1.GetComponentInChildren<GunInfo>(); 
        
        if (itemObject != null)
        {
            slotInfo.UpdateInfo(item, itemObject);
            switch (itemObject.Rarity)
            {
                case Rarities.Common:
                default:
                    slotInfo.gameObject.GetComponent<Image>().color = CommonColor;
                    break;
                case Rarities.Uncommon:
                    slotInfo.gameObject.GetComponent<Image>().color = UncommonColor;
                    break;
                case Rarities.Rare:
                    slotInfo.gameObject.GetComponent<Image>().color = RareColor;
                    break;
                case Rarities.Epic:
                    slotInfo.gameObject.GetComponent<Image>().color = EpicColor;
                    break;
                case Rarities.Legendary:
                    slotInfo.gameObject.GetComponent<Image>().color = LegendaryColor;
                    break;
            }
        }
        else
        {
            slotInfo.ClearInfo();
            slotInfo.gameObject.GetComponent<Image>().color = CommonColor;
        }
    }

    public void UpdateActiveSlot2(Item item, ItemObject itemObject)
    {
        var slotInfo = ActiveSlot2.GetComponentInChildren<GunInfo>();
        if (itemObject != null)
        {
            slotInfo.UpdateInfo(item, itemObject); 
            switch (itemObject.Rarity)
            {
                case Rarities.Common:
                default:
                    slotInfo.gameObject.GetComponent<Image>().color = CommonColor;
                    break;
                case Rarities.Uncommon:
                    slotInfo.gameObject.GetComponent<Image>().color = UncommonColor;
                    break;
                case Rarities.Rare:
                    slotInfo.gameObject.GetComponent<Image>().color = RareColor;
                    break;
                case Rarities.Epic:
                    slotInfo.gameObject.GetComponent<Image>().color = EpicColor;
                    break;
                case Rarities.Legendary:
                    slotInfo.gameObject.GetComponent<Image>().color = LegendaryColor;
                    break;
            }
        }
        else
        {
            slotInfo.ClearInfo();
            slotInfo.gameObject.GetComponent<Image>().color = CommonColor;
        }
    }

    public void ShowHitmarker()
    {
        drawHitmarker = true;
        Invoke("UnshowHitmarker", 0.1f);
    }

    public void UnshowHitmarker()
    {
        drawHitmarker = false;
    }

    private void OnGUI()
    {
        float xMin = (Screen.width / 2) - (Hitmarker.width * hitmarkerScale / 2);
        float yMin = (Screen.height / 2) - (Hitmarker.height * hitmarkerScale / 2);
        if (drawHitmarker)
        {
            GUI.DrawTexture(new Rect(xMin, yMin, Hitmarker.width * hitmarkerScale, Hitmarker.height * hitmarkerScale), Hitmarker);
        }
    }

    public void UpdateHealth(int currentHP)
    {
        playerInfo.UpdateHealth(currentHP);
    }
}
