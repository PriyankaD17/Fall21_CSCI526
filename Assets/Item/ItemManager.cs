using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;   
    public Item currentItem;
    void Awake()
    {
        instance = this;
        currentItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            UseItem();
        }
    }

    public void PickUpItem(Item item)
    {
        currentItem = item;
    }

    public void UseItem()
    {
        currentItem.UseItem();
    }
}
