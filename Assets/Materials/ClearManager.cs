using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour {

	public GameObject Drop_GameObject;
	private Text Result, HighScore;

	// Use this for initialization
	void Start()
	{

		Debug.Log( "currentScore : " + SaveDataManager.GetCurrentScore() + Application.loadedLevelName );
		Debug.Log( "hoge : " + PlayerPrefs.GetInt( "HOGE" ) + Application.loadedLevelName );
		PlayerPrefs.SetInt( "CurrentScore", PlayerPrefs.GetInt( "HOGE" ) );

		Vector3 pos = new Vector3( Random.Range( -1.0f, 1.0f ), 5.3f, 100 );
		Instantiate( Drop_GameObject, pos, new Quaternion() );

		pos = new Vector3( Random.Range( -1.0f, 1.0f ), 7.3f, 100 );
		Instantiate( Drop_GameObject, pos, new Quaternion() );

		pos = new Vector3( Random.Range( -1.0f, 1.0f ), 9.3f, 100 );
		Instantiate( Drop_GameObject, pos, new Quaternion() );

		pos = new Vector3( Random.Range( -1.0f, 1.0f ), 11.3f, 100 );
		Instantiate( Drop_GameObject, pos, new Quaternion() );

		pos = new Vector3( Random.Range( -1.0f, 1.0f ), 13.3f, 100 );
		Instantiate( Drop_GameObject, pos, new Quaternion() );

		if( Application.loadedLevelName == "GiveUp" ) return;

		Result = GameObject.Find( "Result" ).GetComponent<Text>();
		HighScore = GameObject.Find( "HighScore" ).GetComponent<Text>();

		if( SaveDataManager.GetGameMode() == SaveDataManager.GameMode.Chars )
		{
			if( SaveDataManager.GetHighScore( SaveDataManager.GameMode.Chars ) > SaveDataManager.GetCurrentScore() )
			{
				HighScore.text = "ハイスコア！";
				SaveDataManager.SaveScore( SaveDataManager.GetCurrentScore() );
				Result.text = SaveDataManager.GetHighScoreString( SaveDataManager.GetGameMode() );
			}
			else
			{
				HighScore.text = " ";
				Result.text = SaveDataManager.GetCurrentScoreString( SaveDataManager.GetGameMode() ) + "\n" +
								"ハイスコア：" + SaveDataManager.GetHighScoreString( SaveDataManager.GetGameMode() );
			}
		}
		else
		{
			if( SaveDataManager.GetHighScore( SaveDataManager.GetGameMode() ) < SaveDataManager.GetCurrentScore() )
			{
				HighScore.text = "ハイスコア！";
				SaveDataManager.SaveScore();
				Result.text = SaveDataManager.GetHighScoreString( SaveDataManager.GetGameMode() );
			}
			else
			{
				HighScore.text = " ";
				Result.text = SaveDataManager.GetCurrentScoreString( SaveDataManager.GetGameMode() ) + "\n" +
								"ハイスコア：" + SaveDataManager.GetHighScoreString( SaveDataManager.GetGameMode() );
			}
		}


	}

	// Update is called once per frame
	void Update()
	{
		if( Input.GetKeyUp( KeyCode.Escape ) ) Button_GoTitle();
	}

	public void Button_Continue()
	{
		Application.LoadLevel( "main" );
	}

	public void Button_GoTitle()
	{
		Application.LoadLevel( "Title" );
	}

	public void Button_Tweet()
	{
		string str = "";
		str += MainGameManager.GetGameModeString( SaveDataManager.GetGameMode() );
		str += "を";
		str += SaveDataManager.GetHighScoreString( SaveDataManager.GetGameMode() );
		str += "でクリア！";
		str += "\n";
		str += "#UhoUho_Boxy";
		str += "\n";
		str += "https://play.google.com/store/apps/details?id=com.Boxy.UhoUho";
		str += "\n";
		Application.OpenURL( "http://twitter.com/intent/tweet?text=" + WWW.EscapeURL( str ) );
	}

	void OnDestroy()
	{
		SaveDataManager.InitCurrentScore();
	}
	
}
