using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunInfo : MonoBehaviour
{
    [Header("GunInfo")]
    [SerializeField] private TMP_Text gunName;
    [SerializeField] private TMP_Text fireMode;
    [SerializeField] private string defaultFiremodeText;
    [SerializeField] private TMP_Text rarity;
    [SerializeField] private Image gunType;

    [Header("Damage")]
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text damageBuffText;
    [SerializeField] private string defaultDamageText;

    [Header("Recoil")]
    [SerializeField] private TMP_Text recoilText;
    [SerializeField] private TMP_Text recoilBuffText;
    [SerializeField] private string defaultRecoilText;

    [Header("Fire rate")]
    [SerializeField] private TMP_Text fireRateText;
    [SerializeField] private TMP_Text fireRateBuffText;
    [SerializeField] private string defaultFireRateText;

    [Header("Magazine Capacity")]
    [SerializeField] private TMP_Text magazineCapacityText;
    [SerializeField] private TMP_Text magazineCapacityBuffText;
    [SerializeField] private string defaultMagazineCapacityText;

    [Header("Headshot multiplier")]
    [SerializeField] private TMP_Text headshotMultiplierText;
    [SerializeField] private string defaultHeadshotMultiplierText;

    public void UpdateInfo(Item item, ItemObject itemObject)
    {
        gunName.text = itemObject.Name;
        fireMode.text = $"{defaultFiremodeText} {itemObject.FireType}";
        rarity.text = item.Rarity.ToString();
        gunType.sprite = itemObject.GunType == GunTypes.Energy ? UIManager.instance.energyIcon : UIManager.instance.physicalIcon;

        damageText.text = $"{defaultDamageText} {itemObject.GetDamage(item)}";
        recoilText.text = $"{defaultRecoilText} {itemObject.GetRecoilStrength(item)}";
        fireRateText.text = $"{defaultFireRateText} {itemObject.GetRPM(item)}";
        magazineCapacityText.text = $"{defaultMagazineCapacityText} {itemObject.GetMagCapacity(item)}";
        headshotMultiplierText.text = $"{defaultHeadshotMultiplierText} {itemObject.HeadshotMultiplier}";

        damageBuffText.text = $"+{item.buffs[0].value}%";
        recoilBuffText.text = $"-{item.buffs[1].value}%";
        fireRateBuffText.text = $"+{item.buffs[2].value}%";
        magazineCapacityBuffText.text = $"+{item.buffs[3].value}%";
    }

    public void ClearInfo()
    {
        gunName.text = string.Empty;
        fireMode.text = defaultFiremodeText;
        rarity.text = string.Empty;
        gunType.sprite = UIManager.instance.UIMask;

        damageText.text = defaultDamageText;
        recoilText.text = defaultRecoilText;
        fireRateText.text = defaultFireRateText;
        magazineCapacityText.text = defaultMagazineCapacityText;
        headshotMultiplierText.text = defaultHeadshotMultiplierText;

        damageBuffText.text = string.Empty;
        recoilBuffText.text = string.Empty;
        fireRateBuffText.text = string.Empty;
        magazineCapacityBuffText.text = string.Empty;
    }
}
