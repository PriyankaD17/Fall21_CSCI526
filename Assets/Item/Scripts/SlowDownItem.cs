using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownItem: Item
{
    public GameObject slowDownObj;
    public override void PickUpItem()
    {
        base.PickUpItem();
    }

    public override void UseItem()
    {
        base.UseItem();
        GameObject go = Instantiate(slowDownObj, user.transform.position + new Vector3(0, 1, 0) - user.transform.forward * 4, user.transform.rotation);
    }
}
