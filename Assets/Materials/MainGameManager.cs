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
		TimeText = GameObject.Find( "TimeText" ).GetComponent<Text>();
		startTime = Time.time;
		CurrentValText_Subscription = GameObject.Find( "CurrentValText_Subscription" ).GetComponent<Text>();
		CurrentValText_Subscription.text = GetSubscription( SaveDataManager.GetGameMode() );
	}

	// Update is called once per frame
	void Update()
	{
		TimeText.text = ( (int)( TimeLimit + startTime - Time.time ) ).ToString();
		if( Time.time - startTime >= TimeLimit )
		{
			Application.LoadLevel( "TimeUp" );
		}

	}

	public void Button_GiveUp()
	{
		Application.LoadLevel( "GiveUp" );
	}

	void OnDestroy()
	{
		SaveDataManager.OnEnd( Time.time - startTime );
	}

}
