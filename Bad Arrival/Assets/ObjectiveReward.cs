using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveRewardTypes
{
    ACTIVATE_ON_COMPLETE,
    DEACTIVATE_ON_COMPLETE
}
public class ObjectiveReward : MonoBehaviour
{
    public ObjectiveRewardTypes RewardType;
    void Start()
    {
        switch (RewardType)
        {
            case ObjectiveRewardTypes.ACTIVATE_ON_COMPLETE:
                gameObject.SetActive(false);
                break;
            case ObjectiveRewardTypes.DEACTIVATE_ON_COMPLETE:
                gameObject.SetActive(true);
                break;
        }
    }


    public void CompleteObjective()
    {
        switch (RewardType)
        {
            case ObjectiveRewardTypes.ACTIVATE_ON_COMPLETE:
                gameObject.SetActive(true);
                break;
            case ObjectiveRewardTypes.DEACTIVATE_ON_COMPLETE:
                gameObject.SetActive(false);
                break;
        }
    }
}
