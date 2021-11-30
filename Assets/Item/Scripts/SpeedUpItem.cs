using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedUpItem : Item
{
    public ArcadeKart.StatPowerup boostStats = new ArcadeKart.StatPowerup
    {
        MaxTime = 2
    };
    public UnityEvent onPowerupActivated;
    public override void PickUpItem()
    {
        base.PickUpItem();
    }

    public override void UseItem()
    {
        base.UseItem();
        var kart = user.GetComponent<ArcadeKart>();
        if (kart)
        {
            kart.AddPowerup(this.boostStats);
            onPowerupActivated.Invoke();
        }
    }

}
