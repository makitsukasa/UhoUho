using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{

	const int TimeLimit = 60;

	Text TimeText;
	Text TimeText_Subscription;
	Text CurrentValText;
	Text CurrentValText_Subscription;

	SaveDataManager SaveDataManager;

	int currentVal;

	float startTime;

	string GetCurrentValTextSubscription( SaveDataManager.GameMode GameMode )
	{
		//public enum GameMode { Normal, Chars, Banana,  Dummy };
		string[] text =
		{
			"いま\nこ",
			"あと\n文字",
			"いま\nバナナ",
			""
		};
		return text[(int)GameMode];
	}

	string GetTimeTextSubscription( SaveDataManager.GameMode GameMode )
	{
		//public enum GameMode { Normal, Chars, Banana, Dummy };
		string[] text =
		{
			"残り\n秒",
			"いま\n秒",
			"残り\n秒",
			""
		};
		return text[(int)GameMode];
	}

	// Use this for initialization
	void Start()
	{

		SaveDataManager = GameObject.Find( "SaveDataManager" ).GetComponent<SaveDataManager>();
		TimeText = GameObject.Find( "TimeText" ).GetComponent<Text>();
		CurrentValText = GameObject.Find( "CurrentValText" ).GetComponent<Text>();

		CurrentValText_Subscription = GameObject.Find( "CurrentValText_Subscription" ).GetComponent<Text>();
		CurrentValText_Subscription.text = GetCurrentValTextSubscription( SaveDataManager.GetGameMode() );

		TimeText_Subscription = GameObject.Find( "TimeText_Subscription" ).GetComponent<Text>();
		TimeText_Subscription.text = GetTimeTextSubscription( SaveDataManager.GetGameMode() );

		SaveDataManager.InitCurrentScore();

		startTime = Time.time;

	}

	// Update is called once per frame
	void Update()
	{

		if( Input.GetKeyUp( KeyCode.Escape ) ) Button_GiveUp();

		if( SaveDataManager.GetGameMode() == SaveDataManager.GameMode.Chars )
		{
			TimeText.text = ( (int)( Time.time - startTime ) ).ToString();
			CurrentValText.text = ( SaveDataManager.GetTargetScore() - 
									SaveDataManager.GetCurrentScore() ).ToString();
		}
		else {
			TimeText.text = ( (int)( TimeLimit + startTime - Time.time ) ).ToString();
			CurrentValText.text = SaveDataManager.GetCurrentScore().ToString();
			if( Time.time - startTime >= TimeLimit ) Application.LoadLevel( "TimeUp" );
		}

	}

	public void Button_GiveUp()
	{
		Application.LoadLevel( "GiveUp" );
	}

	void OnDestroy()
	{

		if( SaveDataManager.GetGameMode() == SaveDataManager.GameMode.Chars )
		{
			int playtime_ms = (int)( ( Time.time - startTime ) * 1000 );
			SaveDataManager.InitCurrentScore();
			SaveDataManager.AddCurrentScore( playtime_ms );
			SaveDataManager.Save();
		}
		SaveDataManager.OnEnd( Time.time - startTime );
		SaveDataManager.Save();
		Debug.Log( "currentScore : " + SaveDataManager.GetCurrentScore() + Application.loadedLevelName );
		PlayerPrefs.SetInt( "HOGE", SaveDataManager.GetCurrentScore() );
		SaveDataManager.Save();
		Debug.Log( "hoge : " + PlayerPrefs.GetInt("HOGE") + Application.loadedLevelName );
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
