using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayDataFactoty playdata;
    [SerializeField] Text coinText;

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins:" + playdata.playerData.coin.ToString();
    }

    public void PickCoin()
    {
        playdata.playerData.coin++;
        playdata.SavePlayerData();
    }
}
