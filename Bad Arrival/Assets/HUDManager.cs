using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Global")]
    [SerializeField] private Sprite UIMask;
    [SerializeField] private InventoryObject equipped;

    [Header("Weapons")]
    [SerializeField] private GameObject Weapon1;
    [SerializeField] private GameObject Weapon2;
    [Range(0, 1)]
    [SerializeField] private float scalingFactor = 0.75f;

    [Header("Health")]
    [SerializeField] private TMP_Text Health;

    [Header("ProgressBar")]
    [SerializeField] private Slider ProgressBar;
    private CapturePoint target = null;

    public void SetActiveWeapon(int index)
    {
        if (index != 1)
        {
            Weapon1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Weapon2.GetComponent<RectTransform>().localScale = new Vector3(scalingFactor, scalingFactor, 1);
        }
        else
        {
            Weapon1.GetComponent<RectTransform>().localScale = new Vector3(scalingFactor, scalingFactor, 1);
            Weapon2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetSliderTarget(CapturePoint target)
    {
        this.target = target;
    }


    private void OnGUI()
    {
        var guns = equipped.GetSlots;
        if (guns[0].ItemObject != null)
        {
            Weapon1.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = guns[0].ItemObject.uiDisplay;
            Weapon1.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{guns[0].item.ammoInMag} / {guns[0].ItemObject.GetMagCapacity(guns[0].item)}";
        }
        else
        {
            Weapon1.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = UIMask;
            Weapon1.transform.GetChild(1).GetComponent<TMP_Text>().text = $"";
        }
        if (guns[1].ItemObject != null)
        {
            Weapon2.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = guns[1].ItemObject.uiDisplay;
            Weapon2.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{guns[1].item.ammoInMag} / {guns[1].ItemObject.GetMagCapacity(guns[1].item)}";
        }
        else
        {
            Weapon2.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = UIMask;
            Weapon2.transform.GetChild(1).GetComponent<TMP_Text>().text = $"";
        }
        if(target != null)
        {
            ProgressBar.gameObject.SetActive(true);
            ProgressBar.value = target.PercentageFinished;
        } else if(ProgressBar.gameObject.activeSelf && target == null)
        {
            ProgressBar.gameObject.SetActive(false);
        }
    }

    public void UpdateHealth(int currentHP)
    {
        int currentHealth = Mathf.RoundToInt((float)currentHP / (float)Player.instance.MaxHP * 100);
        Health.text = $"{currentHealth} / 100";
    }
}
