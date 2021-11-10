
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
	public int coin;

	public int skin;

	public List<int> mySkins = new List<int>();

	public int mHat;

	public List<int> hats = new List<int>();
	public PlayerData()
	{
		mySkins = new List<int>();
		hats = new List<int>();
		coin = 0;
		skin = 0;
		mySkins.Add(0);
		mHat = 0;
		hats.Add(0);
	}
}
