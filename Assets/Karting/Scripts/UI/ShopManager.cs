using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public List<Button> carBtns;
    public List<Button> hatBtns;
    public List<GameObject> cars;
    public List<GameObject> hats;
    public PlayDataFactoty playDataFactoty;
    public Text txtCoin;
    // Start is called before the first frame update
    void OnEnable()
    {
        playDataFactoty.SavePlayerData();
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].SetActive(false);
        }
        cars[playDataFactoty.playerData.skin].SetActive(true);
        ReFreshUI();
    }


    private void ReFreshUI()
    {
        playDataFactoty.LoadPlayerData();
        for (int i = 0; i < carBtns.Count; i++)
        {
            carBtns[i].GetComponentInChildren<Text>().text = "Buy";
        }
        for (int i = 0; i < playDataFactoty.playerData.mySkins.Count; i++)
        {
            carBtns[playDataFactoty.playerData.mySkins[i]].GetComponentInChildren<Text>().text = "Select";
        }
        carBtns[playDataFactoty.playerData.skin].GetComponentInChildren<Text>().text = "Selected";

        for (int i = 0; i < hatBtns.Count; i++)
        {
            hatBtns[i].GetComponentInChildren<Text>().text = "Buy";
        }
        for (int i = 0; i < playDataFactoty.playerData.hats.Count; i++)
        {
            hatBtns[playDataFactoty.playerData.hats[i]].GetComponentInChildren<Text>().text = "Select";
        }
        hatBtns[playDataFactoty.playerData.mHat].GetComponentInChildren<Text>().text = "Selected";

        txtCoin.text = "Coin: " + playDataFactoty.playerData.coin.ToString();
    }
    public void OnButtonClick(int index)
    {
        Button buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        if (carBtns.Contains(buttonSelf))
        {
            if (carBtns[index].GetComponentInChildren<Text>().text == "Select")
            {
                playDataFactoty.playerData.skin = index;
                for (int i = 0; i < cars.Count; i++)
                {
                    cars[i].SetActive(false);
                }
                cars[playDataFactoty.playerData.skin].SetActive(true);
            }
            else if (carBtns[index].GetComponentInChildren<Text>().text == "Buy" && playDataFactoty.playerData.coin >= index * 100)
            {
                if (!playDataFactoty.playerData.mySkins.Contains(index))
                {
                    playDataFactoty.playerData.mySkins.Add(index);
                    playDataFactoty.playerData.coin -= index * 100;
                }
            }
        }

        else
        {
            if (hatBtns[index].GetComponentInChildren<Text>().text == "Select")
            {
                playDataFactoty.playerData.mHat = index;
                int childCount = GetChild(cars[playDataFactoty.playerData.skin].transform, "HeadEND").childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Destroy(GetChild(cars[playDataFactoty.playerData.skin].transform, "HeadEND").GetChild(0).gameObject);
                }
                GameObject.Instantiate(hats[playDataFactoty.playerData.mHat], GetChild(cars[playDataFactoty.playerData.skin].transform, "HeadEND"));
            }
            else if (hatBtns[index].GetComponentInChildren<Text>().text == "Buy" && playDataFactoty.playerData.coin >= index * 50)
            {
                if (!playDataFactoty.playerData.hats.Contains(index))
                {
                    playDataFactoty.playerData.hats.Add(index);
                    playDataFactoty.playerData.coin -= index * 50;
                }
            }
        }
       
        playDataFactoty.SavePlayerData();
        ReFreshUI();
    }


    public static Transform GetChild(Transform parentTF, string childName)
    {
        //在子物体中查找
        Transform childTF = parentTF.Find(childName);

        if (childTF != null)
        {
            return childTF;
        }
        //将问题交由子物体
        int count = parentTF.childCount;
        for (int i = 0; i < count; i++)
        {
            childTF = GetChild(parentTF.GetChild(i), childName);
            if (childTF != null)
            {
                return childTF;
            }
        }
        return null;
    }
}
