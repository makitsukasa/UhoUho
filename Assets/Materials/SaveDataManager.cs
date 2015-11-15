using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour {

	public enum Key { Gorilla1Num, Gorilla2Num, Gorilla3Num, Gorilla4Num, Gorilla5Num,
						CharNum, BananaNum, Score, PlayNum, PlayTime, Dummy };

	private string GetKeyString( Key key )
	{
		string[] keyStr = {  "Gorilla1Num", "Gorilla2Num", "Gorilla3Num", "Gorilla4Num", "Gorilla5Num",
								"CharNum", "BananaNum", "Score", "PlayNum", "PlayTime", "Dummy" };
		return keyStr[ (int) key ];
	}

	private string GetMessage( Key key )
	{
		return "";
	}

	Text TimeText;

	const int TimeLimit = 30;

	Key currentKey;
	int currentVal;
	int TargetVal;

	float startTime;

	// Use this for initialization
	void Start () {
		TimeText = GameObject.Find( "TimeText" ).GetComponent<Text>();
		startTime = Time.time;
		currentKey = (Key)PlayerPrefs.GetInt( "CurrentKey" );
		currentVal = PlayerPrefs.GetInt( "CurrentVal" );
		TargetVal = PlayerPrefs.GetInt( "TargetVal", 999999999 );
	}
	
	// Update is called once per frame
	void Update () {

		TimeText.text = ( TimeLimit + startTime - (int)( Time.time ) ).ToString();
		if( Time.time - startTime >= 30 )
		{
			Application.LoadLevel( "hoge" );
		}

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

		if( currentKey == key )
		{
			currentVal += value;
			if( currentVal >= TargetVal )
			{
				Application.LoadLevel("hoge");
			}
		}

	}

	public void Save()
	{
		PlayerPrefs.Save();
	}

	void OnDestroy()
	{
		Add( Key.PlayNum );
		Add( Key.PlayTime, (int)( Time.time - startTime ) );
		Save();
	}

}
