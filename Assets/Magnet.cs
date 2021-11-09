using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> coins;

    public float threshodDistance;
    public static float powerUpTime;

    void Update()
    {
        if (powerUpTime>0)
        {
            powerUpTime -= Time.deltaTime;
            foreach(GameObject coin in coins)
            {
                var dist = Vector3.Distance(coin.transform.position, transform.position);
                if (dist < threshodDistance)
                {
                    coin.GetComponent<Coin>().SetCollected();
                    
                } 
                
            }
        }
    }


}
