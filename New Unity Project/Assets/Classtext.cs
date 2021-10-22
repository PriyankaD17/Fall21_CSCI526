using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEditor;

public class Person
{
    public string Name { get; set; }
    public int Score { get; set; }

}
public class PersonList
{
    public Dictionary<string, string> dictionary = new Dictionary<string, string>();
}

public class Classtext : MonoBehaviour
{
    /*定义一个Person对象（其属性包括，Name，HP,Level，Exp,Attak等），
     将其转会成json格式字符串而且写入到person.json的文本中，
     而后将person.json文本中的内容读取出来赋值给新的Person对象。
     */

    public PersonList personList = new PersonList();

    private string jsonstring;
    private JsonData itemData;

    public int GameCount;

    public Dictionary<string, string> dictionarys = new Dictionary<string, string>();


    // Use this for initialization
    void Start()
    {

        //初始化人物信息
        Person person = new Person();
        person.Name = "Czhenya";
        person.Score = 100;

        //调用保存方法
        Save(person);
    }
    /// <summary>
    /// 保存JSON数据到本地的方法
    /// </summary>
    /// <param name="player">要保存的对象</param>
    public void Save(Person player)
    {
        //打包后Resources文件夹不能存储文件，如需打包后使用自行更换目录
        string filePath = Application.dataPath + @"/Resources/JsonPerson.json";
        Debug.Log(Application.dataPath + @"/Resources/JsonPerson.json");

        if (!File.Exists(filePath))  //不存在就建立键值对
        {
            personList.dictionary.Add("Name", player.Name);
            personList.dictionary.Add("HP", player.Score.ToString());

        }
        else   //若存在就更新值
        {
            personList.dictionary["Name"] = player.Name;
            personList.dictionary["HP"] = player.Score.ToString();
        }

        //找到当前路径
        FileInfo file = new FileInfo(filePath);
        //判断有没有文件，有则打开文件，，没有建立后打开文件
        StreamWriter sw = file.CreateText();
        //ToJson接口将你的列表类传进去，，并自动转换为string类型
        string json = JsonMapper.ToJson(personList.dictionary);
        //将转换好的字符串存进文件，
        sw.WriteLine(json);
        //注意释放资源
        sw.Close();
        sw.Dispose();

        AssetDatabase.Refresh();

    }

    /// <summary>
    /// 读取保存数据的方法
    /// </summary>
    public void LoadPerson()
    {
        //调试用的  //Debug.Log(1);

        //TextAsset该类是用来读取配置文件的
        TextAsset asset = Resources.Load("JsonPerson") as TextAsset;
        if (!asset)  //读不到就退出此方法
            return;

        string strdata = asset.text;
        JsonData jsdata3 = JsonMapper.ToObject(strdata);
        //在这里循环输出表示读到了数据，，即此数据能够使用了
        for (int i = 0; i < jsdata3.Count; i++)
        {
            Debug.Log(jsdata3[i]);
        }
        Person person = new Person();
        person.Name = jsdata3[0].ToString();
        person.Score =int.Parse(jsdata3[1].ToString());
        Debug.Log(person.Name);
        Debug.Log(person.Score.ToString());
        //使用foreach输出的话会以[键，值]，，，
        /*foreach (var item in jsdata3)
        {
            Debug.Log(item);
        }*/

    }

    private void OnGUI()
    {   //点击读取存储的文件
        if (GUILayout.Button("LoadTXT"))
        {
            LoadPerson();
        }
    }
}
