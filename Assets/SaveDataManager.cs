using UnityEngine;
using System.Collections;

public class SaveDataManager : MonoBehaviour {

	public enum Key { Gorilla1Num, Gorilla2Num, Gorilla3Num, Gorilla4Num, Gorilla5Num,
						CharNum, BananaNum, Score, PlayNum, PlayTime };

	private string GetKeyString( Key key )
	{
		string[] keyStr = {  "Gorilla1Num", "Gorilla2Num", "Gorilla3Num", "Gorilla4Num", "Gorilla5Num",
							"CharNum", "BananaNum", "Score", "PlayNum", "PlayTime" };
		return keyStr[ (int) key ];
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int Get( Key key )
	{
		return PlayerPrefs.GetInt( GetKeyString( key ) );
	}

	public void Add( Key key, int value = 1 )
	{
		int val = PlayerPrefs.GetInt( GetKeyString( key ) ) + value;
		if( val >= 999999999 ) val = 999999999;
		PlayerPrefs.SetInt( GetKeyString( key ), val );
	}

	public void Save()
	{
		PlayerPrefs.Save();
	}

	void OnDestroy()
	{
		Save();
	}

}
