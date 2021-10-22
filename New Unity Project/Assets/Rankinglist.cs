using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rankinglist : MonoBehaviour
{
    public JsonDL classtext;

    public GameObject PaiPrefab;
    public int Index;

    public int IndexMax=0;
    public int IndexmIN;


    private void Start()
    {
        classtext = GameObject.Find("GameManager").GetComponent<JsonDL>();
        classtext.Rankinglist();
        Index = classtext.Indexmax;
        IndexmIN = Index;
        for (int i = 0; i < classtext.Indexmax+ classtext.Indexmin; i++)
        {
            Instantiate(PaiPrefab).transform.SetParent(this.transform);
        }
        foreach (var item in classtext.dictionarys)
        {
            if (item.Value.Score == 100)
            {
                transform.GetChild(IndexMax).GetChild(0).GetComponent<Text>().text = "名字：" + item.Value.Name + "       " + "分数:" + item.Value.Score;
                IndexMax++;
            }
            else
            {
                transform.GetChild(IndexmIN).GetChild(0).GetComponent<Text>().text = "名字：" + item.Value.Name + "       " + "分数:" + item.Value.Score;
                IndexmIN++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
