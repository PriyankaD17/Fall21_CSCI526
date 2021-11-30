using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;   
    public Item currentItem;
    public Image itemBtn;
    void Awake()
    {
        instance = this;
        currentItem = null;
        itemBtn.gameObject.SetActive(false);
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
        if (currentItem == null)
        {
            currentItem = item;
            itemBtn.gameObject.SetActive(true);
            itemBtn.sprite = item.icon;
        }
    }

    public void UseItem()
    {
        if (currentItem!=null)
        {
            currentItem.UseItem();
            itemBtn.gameObject.SetActive(false);
            currentItem = null;
        }
  
    }
}
