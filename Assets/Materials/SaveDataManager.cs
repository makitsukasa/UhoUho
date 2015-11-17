﻿using UnityEngine;
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

	public enum GameMode { Normal, Chars, Banana, Dummy };

	public static string GetGameModeString( GameMode key )
	{
		string[] keyStr = { "Normal", "Chars", "Banana", "Dummy" };
		return keyStr[(int)key];
	}
	
	// Use this for initialization
	void Start ()
	{
		Debug.Log( GetGameMode() + " in start" );
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Add( Key key, int value = 1 )
	{
		//累計個数をカウントしておく
		int val = PlayerPrefs.GetInt( GetKeyString( key ) ) + value;
		if( val >= 999999999 ) val = 999999999;
		PlayerPrefs.SetInt( GetKeyString( key ), val );

		//ついでにいろいろする
		switch( GetGameMode() )
		{
		case GameMode.Normal:
			if( key == Key.CharNum ) AddCurrentScore();
			break;
		case GameMode.Chars:
			if( key == Key.CharNum ) AddCurrentScore();
			if( GetCurrentScore() >= GetTargetScore() )
			{
				Debug.Log( "currentScore : " + GetCurrentScore() );
				Application.LoadLevel( "Clear" );
			}
			break;
		case GameMode.Banana:
			if( key == Key.BananaNum ) AddCurrentScore();
			break;
		default:
			break;
		}

	}

	public static void SaveScore( int time_ms_for_Chars = 999999999 )
	{

		if( GetGameMode() == GameMode.Chars )
		{
			string str = "HighScore" + GetGameMode().ToString();
			PlayerPrefs.SetInt( str, min( time_ms_for_Chars, PlayerPrefs.GetInt( str, 999999999 ) ) );
			Debug.Log( str + "  " + PlayerPrefs.GetInt( str ) );
		}
		else
		{
			string str = "HighScore" + GetGameMode().ToString();
			PlayerPrefs.SetInt( str, max( GetCurrentScore(), PlayerPrefs.GetInt( str ) ) );
			Debug.Log( str + "  " + PlayerPrefs.GetInt( str ) );
		}

		Save();
		
	}

	public static void SetGameMode( GameMode key, int targetScore = 999999999 )
	{
		PlayerPrefs.SetInt( "GameMode", (int)key );
		PlayerPrefs.SetInt( "TargetScore", targetScore );
		//InitCurrentScore();
		Debug.Log( key + " save" );
		Save();
	}

	public static GameMode GetGameMode()
	{
		return (GameMode)PlayerPrefs.GetInt( "GameMode" );
	}

	public static int GetCurrentScore()
	{
		return PlayerPrefs.GetInt( "CurrentScore" );
	}

	public static void AddCurrentScore( int val = 1 )
	{
		PlayerPrefs.SetInt( "CurrentScore", PlayerPrefs.GetInt( "CurrentScore" ) + val );
	}

	public static void InitCurrentScore()
	{
		PlayerPrefs.SetInt( "CurrentScore", 0 );
	}


	public int GetTargetScore()
	{
		return PlayerPrefs.GetInt( "TargetScore" );
	}

	public static int GetHighScore( GameMode mode )
	{
		string str = "HighScore" + mode.ToString();
		if( mode == GameMode.Chars )
		{
			return PlayerPrefs.GetInt( str ,999999999 );
		}
		else
		{
			return PlayerPrefs.GetInt( str );
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

	public int Get( Key key )
	{
		return PlayerPrefs.GetInt( GetKeyString( key ) );
	}

	void OnDestroy()
	{
		Save();
		Debug.Log( "currentScore : " + GetCurrentScore() + Application.loadedLevelName );
	}

	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	public static void Save()
	{
		PlayerPrefs.Save();
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

	void OnApplicationQuit()
	{
		InitCurrentScore();
	}

}
