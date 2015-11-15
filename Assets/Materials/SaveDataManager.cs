using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour {

	public enum Key { Gorilla1Num, Gorilla2Num, Gorilla3Num, Gorilla4Num, Gorilla5Num,
						CharNum, BananaNum, Score, PlayNum, PlayTime, Dummy};

	public static string GetKeyString( Key key )
	{
		string[] keyStr = {  "Gorilla1Num", "Gorilla2Num", "Gorilla3Num", "Gorilla4Num", "Gorilla5Num",
								"CharNum", "BananaNum", "Score", "PlayNum", "PlayTime", "Dummy" };
		return keyStr[ (int) key ];
	}

	public enum GameMode { Normal, GorillaGorilla, Chars, Banana, Dummy };

	public static string GetGameModeString( GameMode key )
	{
		string[] keyStr = { "Normal", "GorillaGorilla", "Chars", "Banana", "Dummy" };
		return keyStr[(int)key];
	}

	GameMode currentGameMode;
	int currentVal = 0;
	int TargetVal;

	// Use this for initialization
	void Start () {
		currentGameMode = (GameMode)PlayerPrefs.GetInt( "GameMode" );
		TargetVal = PlayerPrefs.GetInt( "TargetVal", 999999999 );
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

		switch( currentGameMode )
		{
		case GameMode.Normal:
			break;
		case GameMode.GorillaGorilla:
			if( key == Key.Gorilla2Num || key == Key.Gorilla3Num ||
				key == Key.Gorilla4Num || key == Key.Gorilla5Num )
				currentVal++;
			break;
		case GameMode.Chars:
			if( key == Key.CharNum ) currentVal++;
			if( currentVal >= TargetVal ) Application.LoadLevel( "Clear" );
			break;
		case GameMode.Banana:
			if( key == Key.BananaNum ) currentVal++;
			break;
		default:
			break;
		}

	}

	public static void OnEnd( float playtime )
	{
		string str = GetKeyString( Key.PlayNum );
		PlayerPrefs.SetInt( str, PlayerPrefs.GetInt( str ) + 1 );

		str = GetKeyString( Key.PlayTime );
		PlayerPrefs.SetInt( str, PlayerPrefs.GetInt( str ) + (int)playtime );
    }

	public static void SetGameMode( GameMode key, int targetVal = 999999999 )
	{
		PlayerPrefs.SetInt( "GameMode", (int)key );
		PlayerPrefs.SetInt( "TargetVal", targetVal );
		Save();
	}

	public static GameMode GetGameMode()
	{
		return (GameMode)PlayerPrefs.GetInt( "GameMode");
	}
	
	public static void Save()
	{
		PlayerPrefs.Save();
	}

	void OnDestroy()
	{
		Save();
	}

}
