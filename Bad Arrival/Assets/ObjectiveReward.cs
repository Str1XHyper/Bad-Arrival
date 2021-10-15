using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveRewardTypes{
    DOOR,
    ITEM,
    CHEST,
}
public class ObjectiveReward : MonoBehaviour
{
    public ObjectiveRewardTypes RewardType;
    void Start()
    {
        switch (RewardType)
        {
            case ObjectiveRewardTypes.DOOR:
                gameObject.SetActive(true);
                break;
            case ObjectiveRewardTypes.ITEM:
                gameObject.SetActive(false);
                break;
            case ObjectiveRewardTypes.CHEST:
                gameObject.SetActive(false);
                break;
        }
    }

    public void CompleteObjective()
    {
        switch (RewardType)
        {
            case ObjectiveRewardTypes.DOOR:
                gameObject.SetActive(false);
                break;
            case ObjectiveRewardTypes.ITEM:
                gameObject.SetActive(true);
                break;
            case ObjectiveRewardTypes.CHEST:
                gameObject.SetActive(true);
                break;
        }
    }
}