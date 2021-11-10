using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> cars;
    public List<GameObject> hats;
    public PlayDataFactoty playDataFactoty;
    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        playDataFactoty.LoadPlayerData();
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].SetActive(false);
        }
        cars[playDataFactoty.playerData.skin].SetActive(true);
        
        int childCount = GetChild(cars[playDataFactoty.playerData.skin].transform, "HeadEND").childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(GetChild(cars[playDataFactoty.playerData.skin].transform, "HeadEND").GetChild(0).gameObject);
        }
        GameObject.Instantiate(hats[playDataFactoty.playerData.mHat], GetChild(cars[playDataFactoty.playerData.skin].transform, "HeadEND"));

        if (virtualCamera)
        {
            virtualCamera.Follow = cars[playDataFactoty.playerData.skin].transform;
            virtualCamera.LookAt = cars[playDataFactoty.playerData.skin].transform.Find("KartBouncingCapsule");
        }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Transform GetChild(Transform parentTF, string childName)
    {
        Transform childTF = parentTF.Find(childName);

        if (childTF != null)
        {
            return childTF;
        }
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
