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

	string GetSubscription( SaveDataManager.GameMode GameMode )
	{
		//public enum GameMode { Normal, GorillaGorilla, Chars, Banana, Dummy };
		string[] text =
		{
			"",
			"いま\nこ",
			"あと\n文字",
			"いま\nバナナ",
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
		startTime = Time.time;
		CurrentValText_Subscription = GameObject.Find( "CurrentValText_Subscription" ).GetComponent<Text>();
		CurrentValText_Subscription.text = GetSubscription( SaveDataManager.GetGameMode() );
	}

	// Update is called once per frame
	void Update()
	{
		if( SaveDataManager.GetGameMode() == SaveDataManager.GameMode.Chars )
		{
			TimeText.text = ( (int)( Time.time - startTime ) ).ToString();
		}
		else {
			TimeText.text = ( (int)( TimeLimit + startTime - Time.time ) ).ToString();
			if( Time.time - startTime >= TimeLimit )
			{
				Application.LoadLevel( "TimeUp" );
			}
		}

		CurrentValText.text = SaveDataManager.GetCurrentScore().ToString();
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
			SaveDataManager.SaveScore( playtime_ms );
		}
		else
		{
			SaveDataManager.SaveScore();
		}

		SaveDataManager.OnEnd( Time.time - startTime );

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
