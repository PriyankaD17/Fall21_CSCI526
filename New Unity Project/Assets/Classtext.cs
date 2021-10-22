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
    /*����һ��Person���������԰�����Name��HP,Level��Exp,Attak�ȣ���
     ����ת���json��ʽ�ַ�������д�뵽person.json���ı��У�
     ����person.json�ı��е����ݶ�ȡ������ֵ���µ�Person����
     */

    public PersonList personList = new PersonList();

    private string jsonstring;
    private JsonData itemData;

    public int GameCount;

    public Dictionary<string, string> dictionarys = new Dictionary<string, string>();


    // Use this for initialization
    void Start()
    {

        //��ʼ��������Ϣ
        Person person = new Person();
        person.Name = "Czhenya";
        person.Score = 100;

        //���ñ��淽��
        Save(person);
    }
    /// <summary>
    /// ����JSON���ݵ����صķ���
    /// </summary>
    /// <param name="player">Ҫ����Ķ���</param>
    public void Save(Person player)
    {
        //�����Resources�ļ��в��ܴ洢�ļ�����������ʹ�����и���Ŀ¼
        string filePath = Application.dataPath + @"/Resources/JsonPerson.json";
        Debug.Log(Application.dataPath + @"/Resources/JsonPerson.json");

        if (!File.Exists(filePath))  //�����ھͽ�����ֵ��
        {
            personList.dictionary.Add("Name", player.Name);
            personList.dictionary.Add("HP", player.Score.ToString());

        }
        else   //�����ھ͸���ֵ
        {
            personList.dictionary["Name"] = player.Name;
            personList.dictionary["HP"] = player.Score.ToString();
        }

        //�ҵ���ǰ·��
        FileInfo file = new FileInfo(filePath);
        //�ж���û���ļ���������ļ�����û�н�������ļ�
        StreamWriter sw = file.CreateText();
        //ToJson�ӿڽ�����б��ഫ��ȥ�������Զ�ת��Ϊstring����
        string json = JsonMapper.ToJson(personList.dictionary);
        //��ת���õ��ַ�������ļ���
        sw.WriteLine(json);
        //ע���ͷ���Դ
        sw.Close();
        sw.Dispose();

        AssetDatabase.Refresh();

    }

    /// <summary>
    /// ��ȡ�������ݵķ���
    /// </summary>
    public void LoadPerson()
    {
        //�����õ�  //Debug.Log(1);

        //TextAsset������������ȡ�����ļ���
        TextAsset asset = Resources.Load("JsonPerson") as TextAsset;
        if (!asset)  //���������˳��˷���
            return;

        string strdata = asset.text;
        JsonData jsdata3 = JsonMapper.ToObject(strdata);
        //������ѭ�������ʾ���������ݣ������������ܹ�ʹ����
        for (int i = 0; i < jsdata3.Count; i++)
        {
            Debug.Log(jsdata3[i]);
        }
        Person person = new Person();
        person.Name = jsdata3[0].ToString();
        person.Score =int.Parse(jsdata3[1].ToString());
        Debug.Log(person.Name);
        Debug.Log(person.Score.ToString());
        //ʹ��foreach����Ļ�����[����ֵ]������
        /*foreach (var item in jsdata3)
        {
            Debug.Log(item);
        }*/

    }

    private void OnGUI()
    {   //�����ȡ�洢���ļ�
        if (GUILayout.Button("LoadTXT"))
        {
            LoadPerson();
        }
    }
}
