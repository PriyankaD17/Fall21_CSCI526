using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayDataFactoty: MonoBehaviour
{
	private string payerDate_path;

	public PlayerData playerData;

	private void Awake()
	{
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			payerDate_path = Application.streamingAssetsPath + "/playerData";
		}
		else
		{
			payerDate_path = Application.persistentDataPath + "/playerData";
		}
		LoadPlayerData();
	}

	public void LoadPlayerData()
	{
		if (File.Exists(payerDate_path))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(payerDate_path, FileMode.Open);
			playerData = (PlayerData)bf.Deserialize(file);
			file.Close();
		}
		else
		{
			playerData = new PlayerData();
		}
	}
  
	public void SavePlayerData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		if (File.Exists(payerDate_path))
		{
			File.Delete(payerDate_path);
		}
		FileStream file = File.Create(payerDate_path);
		bf.Serialize(file, playerData);
		file.Close();
	}

}
