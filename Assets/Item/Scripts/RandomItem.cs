using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    private SlowDownItem SlowDownItem;
    private ObstacleItem obstacleItem;
    private SpeedUpItem SpeedUpItem;
    // Start is called before the first frame update
    void Start()
    {
        SlowDownItem = gameObject.GetComponent<SlowDownItem>();
        obstacleItem = gameObject.GetComponent<ObstacleItem>();
        SpeedUpItem = gameObject.GetComponent<SpeedUpItem>();
        SlowDownItem.enabled = false;
        obstacleItem.enabled = false;
        SpeedUpItem.enabled = false;
        int itemType = Random.Range(0, 3);
        switch (itemType)
        {
            case 0:
                SlowDownItem.enabled = true;
                break;
            case 1:
                obstacleItem.enabled = true;
                break;
            case 2:
                SpeedUpItem.enabled = true;
                break;
            default:
                break;
        }
    }

}
