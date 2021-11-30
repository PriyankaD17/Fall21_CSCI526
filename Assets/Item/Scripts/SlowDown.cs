using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public ArcadeKart.StatPowerup boostStats = new ArcadeKart.StatPowerup
    {
        MaxTime = 2
    };
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        var rb = other.attachedRigidbody;
        if (rb)
        {
            var kart = rb.GetComponent<ArcadeKart>();
            if (kart)
            {
                kart.AddPowerup(this.boostStats);
                this.gameObject.SetActive(false);
            }
        }
    }
}
