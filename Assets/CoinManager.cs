using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int coinCount;
    [SerializeField] Text coinText;

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins:" + coinCount;
    }
}
