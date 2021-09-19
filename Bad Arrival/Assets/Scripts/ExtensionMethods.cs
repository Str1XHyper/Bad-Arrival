

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        Color CommonColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        Color UncommonColor = new Color(0.5f, 0.7f, 0.5f, 0.5f);
        Color RareColor = new Color(0.5f, 0.6f, 0.7f, 0.5f);
        Color EpicColor = new Color(0.6f, 0.5f, 0.7f, 0.5f);
        Color LegendaryColor = new Color(0.7f, 0.6f, 0.5f, 0.5f);
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            if (_slot.Value.item.Id >= 0)
            {
                Color bgColor;
                switch (_slot.Value.ItemObject.Rarity)
                {
                    case Rarities.Common:
                    default:
                        bgColor = UIManager.instance.CommonColor;
                        break;
                    case Rarities.Uncommon:
                        bgColor = UIManager.instance.UncommonColor;
                        break;
                    case Rarities.Rare:
                        bgColor = UIManager.instance.RareColor;
                        break;
                    case Rarities.Epic:
                        bgColor = UIManager.instance.EpicColor;
                        break;
                    case Rarities.Legendary:
                        bgColor = UIManager.instance.LegendaryColor;
                        break;
                }
                _slot.Key.GetComponent<Image>().color = bgColor;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.GetComponent<Image>().color = CommonColor;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public static decimal Map(this decimal value, decimal fromSource, decimal toSource, decimal fromTarget, decimal toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
