using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public virtual void PickUpItem()
    {
        ItemManager.instance.PickUpItem(this);
    }

    public virtual void UseItem()
    {

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            PickUpItem();
        }
        else if (collision.gameObject.tag == "AI")
        {
            UseItem();
        }
        Destroy(this.gameObject);
    }
}
