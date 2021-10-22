using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragCube : MonoBehaviour
{
    private Vector2 startPos;
    [SerializeField] private Transform correctTrans;
    [SerializeField] private bool isFinished;
    [SerializeField] private Image imgtrue;
    [SerializeField] private Image imgfalse;

    public JsonDL classtext;

    public GameObject Victory;

    private void Start()
    {
        startPos = transform.position;
        classtext = GameObject.Find("GameManager").GetComponent<JsonDL>();
    }

    private void OnMouseDrag()
    {
        if (!isFinished)
        {
            transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);        
        }
        imgtrue.gameObject.SetActive(false);
        imgfalse.gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - correctTrans.position.x) <= 1f && Mathf.Abs(transform.position.y - correctTrans.position.y) <= 1f)
        {
            transform.position = new Vector2(correctTrans.position.x, correctTrans.position.y);
            isFinished = true;
            ShowTruetips();
        }
        else
        {
            transform.position = new Vector2(startPos.x, startPos.y);
            ShowFalsetips();
        }
    }

    private void ShowTruetips()
    {
        imgtrue.gameObject.SetActive(true);
        classtext.GameCount++;
        if (classtext.GameCount>=4)
        {
            classtext.LoadJsons();
            foreach (var item in classtext.dictionarys)
            {
                if(item.Value.Name== classtext.Names)
                {
                    item.Value.Score = 100;
                    classtext.SaveVer();
                    return;
                }
            }
            Victory.SetActive(true);
            classtext.GameCount = 0;
        }
    }
    private void ShowFalsetips()
    {
        imgfalse.gameObject.SetActive(true);

    }

    public void Waive()
    {
        foreach (var item in classtext.dictionarys)
        {
            if (item.Value.Name == classtext.Names)
            {
                item.Value.Score = 0;
                classtext.SaveVer();
                return;
            }
        }
        SceneManager.LoadScene("Select Scene");
    }

}
