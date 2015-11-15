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
	int currentScore = 0;
	int TargetScore;


	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll();
		currentGameMode = (GameMode)PlayerPrefs.GetInt( "GameMode" );
		TargetScore = PlayerPrefs.GetInt( "TargetVal", 999999999 );
		Debug.Log( currentGameMode.ToString() + GetGameMode() );
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
		//累計個数をカウントしておく
		int val = PlayerPrefs.GetInt( GetKeyString( key ) ) + value;
		if( val >= 999999999 ) val = 999999999;
		PlayerPrefs.SetInt( GetKeyString( key ), val );

		//ついでにいろいろする
		switch( currentGameMode )
		{
		case GameMode.Normal:
			if( key == Key.CharNum ) currentScore++;
			break;
		case GameMode.GorillaGorilla:
			if( key == Key.Gorilla2Num || key == Key.Gorilla3Num ||
				key == Key.Gorilla4Num || key == Key.Gorilla5Num )
				currentScore++;
			break;
		case GameMode.Chars:
			if( key == Key.CharNum ) currentScore++;
			if( currentScore >= TargetScore ) Application.LoadLevel( "Clear" );
			break;
		case GameMode.Banana:
			if( key == Key.BananaNum ) currentScore++;
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

		Save();
    }

	public static void SetGameMode( GameMode key, int targetVal = 999999999 )
	{
		PlayerPrefs.SetInt( "GameMode", (int)key );
		PlayerPrefs.SetInt( "TargetVal", targetVal );
		Save();
	}

	public void SaveScore( int time_ms_for_Chars = 0 )
	{
		string str;

		if( GetGameMode() == GameMode.Chars )
		{
			str = "HighScore" + GetGameMode().ToString();
			PlayerPrefs.SetInt( str, min( time_ms_for_Chars, PlayerPrefs.GetInt( str, 999999999 ) ) );

			str = "CurrentScore" + GetGameMode().ToString();
			PlayerPrefs.SetInt( str, time_ms_for_Chars );
		}
		else
		{
			str = "HighScore" + GetGameMode().ToString();
			PlayerPrefs.SetInt( str, max( currentScore, PlayerPrefs.GetInt( str ) ) );

			str = "CurrentScore" + GetGameMode().ToString();
			PlayerPrefs.SetInt( str, currentScore );
		}

	}

	public int GetCurrentScore()
	{
		return currentScore;
	}

	public static GameMode GetGameMode()
	{
		return (GameMode)PlayerPrefs.GetInt( "GameMode" );
	}
	
	public static void Save()
	{
		PlayerPrefs.Save();
	}

	void OnDestroy()
	{
		Save();
	}

	static int max( int a, int b )
	{
		if( a > b ) return a;
		return b;
	}

	static int min( int a, int b )
	{
		if( a < b ) return a;
		return b;
	}


}
