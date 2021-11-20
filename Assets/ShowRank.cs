using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowRank : MonoBehaviour
{
    public Text rank;
    public PlayDataFactoty dataFactoty;
    // Start is called before the first frame update
    void Start()
    {
        if (dataFactoty.playerData.Rank>0)
        {
            rank.gameObject.SetActive(true);
            rank.text = "No." + dataFactoty.playerData.Rank.ToString();
        }
        else
        {
            rank.gameObject.SetActive(false);
        }     
    }
}
