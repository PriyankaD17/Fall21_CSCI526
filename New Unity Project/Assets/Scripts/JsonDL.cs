using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class User
{
    public int Score { get; set; }
    public string Name { get; set; }
    public string PassWord { get; set; }
}


public class JsonDL : MonoBehaviour
{
    public string Names;
    public string PassWords;

    public int Indexmax;
    public int Indexmin;

    //public GameObject change;
    public InputField nameInput;
    public InputField passwordInput;
    public Button signUpButton;
    public Button signInButton;

    private string jsonFilePath;
    private JSONObject _jsonObject;


    public int GameCount;

    public Dictionary<string, User> dictionarys = new Dictionary<string, User>();

    //public GameObject errorPanel;
    //public GameObject registerPanel;
    public Dictionary<string, User> dic = new Dictionary<string, User>();
    void Start()
    {
        DontDestroyOnLoad(this);
        //change.GetComponent<ChangeSence>().enabled = false;
        signUpButton.onClick.AddListener(SignUp);
        signInButton.onClick.AddListener(SignIn);
        jsonFilePath = Application.dataPath + "/Resources/json.txt";
    }
    public void Rankinglist()
    {
        LoadJsons();
        foreach (var item in dictionarys)
        {
            if (item.Value.Score == 100)
            {
                Indexmax++;
            }
            else
            {
                Indexmin++;
            }
        }
    }

    public void LoadJsons()
    {
        dictionarys = LoadJson();

    }

    public void LoadJsonss()
    {
        _jsonObject.Clear();
    }

    /// <summary>
    /// 读取json文件，保存到集合
    /// </summary>

    public Dictionary<string ,User> LoadJson()
    {
        dic.Clear();
        TextAsset txt = Resources.Load<TextAsset>("json");
        _jsonObject = new JSONObject(txt == null ? "[]" : txt.text);//保证读取的文本不为空

        foreach(var data in _jsonObject.list)
        {
            User user = new User();
            user.Score = (int)data["Id"].i;
            user.Name = data["Name"].str;
            user.PassWord = data["Password"].str;
            dic.Add(user.Name, user);
        }
        return dic;
    }

    /// <summary>
    /// 修改Json文本
    /// </summary>
    /// <param name="url">json的路径</param>
    /// <param name="str">要修改的json内容</param>
    public void SaveVer()
    {
        string filePath = Application.dataPath + "/Resources/json.txt";
        FileInfo file = new FileInfo(filePath);
        string jsonStr = "";
        jsonStr += "[\n";
        foreach (var item in dictionarys)
        {
            Debug.Log(item.Value.Name);
             jsonStr += "{" + string.Format(@"""Id"":{0},""Name"":""{1}"",""Password"":""{2}""", item.Value.Score,
         item.Value.Name, item.Value.PassWord) + "}";
            jsonStr += ",";  //逗号做分割
            jsonStr += "\n";
        }
        jsonStr += "]";
        StreamWriter sw = file.CreateText();
        sw.Write(jsonStr);
        sw.Close();
        AssetDatabase.Refresh();
    }


    /// <summary>
    /// 存储json文件，覆盖原文件
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    public void SaveJson(int Score)
    {
       // _jsonObject.Clear();
        string jsonObjectStr = "{" + string.Format(@"""Id"":{0},""Name"":""{1}"",""Password"":""{2}""", Score,
            Names, PassWords) + "}";
        //原字符串为:{"Id":1,"Name":"str1","PassWord":"str1"}
        _jsonObject.Add(new JSONObject(jsonObjectStr));//往json列表里添加一条记录
       // _jsonObject.Copy();//往json列表里添加一条记录
        string jsonStr = "";
        jsonStr += "[\n";
        for(int i=0;i<_jsonObject.list.Count;i++)
        {
            jsonStr += _jsonObject.list[i];
            if (i < _jsonObject.list.Count - 1)
                jsonStr += ",";  //逗号做分割
            jsonStr += "\n";
        }
        jsonStr += "]";
        File.WriteAllText(jsonFilePath, jsonStr);
        AssetDatabase.Refresh();//刷新unity内资源
    }


    /// <summary>
    /// 注册
    /// </summary>
    public void SignUp()
    {
        //SaveVer();
        if (string.IsNullOrEmpty(nameInput.text)||string.IsNullOrEmpty(passwordInput.text))
        {
            Debug.Log("输入不能为空");
            return;
        }
        if (LoadJson().ContainsKey(nameInput.text)) {
            
            Debug.Log("用户名已存在");
        }
            
        else
        {
            Names = nameInput.text;
            PassWords = passwordInput.text;
            SaveJson(0);
            //registerPanel.SetActive(true);
            Debug.Log("注册成功");
        }
    }
    /// <summary>
    /// 登录
    /// </summary>
    public void SignIn()
    {
       // Debug.Log(dic[nameInput.text].PassWord);

        if (string .IsNullOrEmpty(nameInput.text)||string.IsNullOrEmpty(passwordInput.text))
        {
            Debug.Log("输入不能为空");
            return;
        }
        if (!LoadJson().ContainsKey(nameInput.text))
        {
            //errorPanel.SetActive(true);
            Debug.Log("不存在");

        }
        else
        {
            foreach (var item in LoadJson())
            {
                User use = item.Value;
                if (use.Name== nameInput.text&& use.PassWord == passwordInput.text)
                {
                    Names = nameInput.text;
                    PassWords = passwordInput.text;

                    Debug.Log("登录成功");
                    SceneManager.LoadScene("Select Scene");
                }
            }
            //change.GetComponent<ChangeSence>().LoadScene("Halloween_Level");

        }
        
    }

    public void closeerrerpanel()
    {
        //errorPanel.SetActive(false);
    }
    public void closeregisterPnael()
    {
        //registerPanel.SetActive(false);
    }
    void Update()
    {
        
    }
}
