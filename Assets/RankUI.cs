using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankUI : MonoBehaviour
{
    public List<Text> rank;
    public RoadColliderGroupManager roadColliderGroupManager;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rank.Count; i++)
        {
            if (roadColliderGroupManager.AllPlayersRankResult[i])
            {
                rank[i].text = roadColliderGroupManager.AllPlayersRankResult[i].name;
            }          
        }
    }
}
