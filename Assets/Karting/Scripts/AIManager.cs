using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<ArcadeKart> AIs;
    public PlayDataFactoty playDataFactoty;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in AIs)
        {
            switch (playDataFactoty.playerData.gameMode)
            {
                case 0:
                    item.baseStats.TopSpeed = 7;
                    break;
                case 1:
                    item.baseStats.TopSpeed = 10;
                    break;
                case 2:
                    item.baseStats.TopSpeed = 15;
                    break;
                default:
                    break;
            } 
        }
    }
}
