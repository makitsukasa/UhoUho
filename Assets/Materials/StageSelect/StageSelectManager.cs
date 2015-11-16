using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour {

	public GameObject Drop_GameObject;

	private Text HighScoreText_Normal;
	private Text HighScoreText_GorillaGorilla;
	private Text HighScoreText_Chars;
	private Text HighScoreText_Banana;

	// Use this for initialization
	void Start()
	{

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

		HighScoreText_Normal			= GameObject.Find( "HighScore_Normal" )			.GetComponent<Text>();
		HighScoreText_Chars				= GameObject.Find( "HighScore_Chars" )			.GetComponent<Text>();
		HighScoreText_Banana			= GameObject.Find( "HighScore_Banana" )			.GetComponent<Text>();

		string highScore = PlayerPrefs.GetInt( "HighScoreNormal" ).ToString() + "こ";
		if( highScore == "0" ) highScore = "-";
		HighScoreText_Normal.text = "ハイスコア：\n" + highScore;

		highScore = ( PlayerPrefs.GetInt( "HighScoreChars", 999999999 ) / 1000 ).ToString() + "秒" +
					( PlayerPrefs.GetInt( "HighScoreChars", 999999999 ) % 1000 ).ToString();
		if( highScore == "999999秒999" ) highScore = "-";
		HighScoreText_Chars.text = "ハイスコア：\n" + highScore;

		highScore = PlayerPrefs.GetInt( "HighScoreBanana" ).ToString() + "バナナ";
		if( highScore == "0" ) highScore = "-";
		HighScoreText_Banana.text = "ハイスコア：\n" + highScore;

	}

	// Update is called once per frame
	void Update () {
		if( Input.GetKeyUp( KeyCode.Escape ) ) Button_Quit();
	}

	public void Button_Normal()
	{
		SaveDataManager.SetGameMode( SaveDataManager.GameMode.Normal );
		Application.LoadLevel( "main" );
	}

	public void Button_GorillaGorilla()
	{
		SaveDataManager.SetGameMode( SaveDataManager.GameMode.GorillaGorilla );
		Application.LoadLevel( "main" );
	}

	public void Button_Chars()
	{
		SaveDataManager.SetGameMode( SaveDataManager.GameMode.Chars, 100 );
		Application.LoadLevel( "main" );
	}

	public void Button_Banana()
	{
		SaveDataManager.SetGameMode( SaveDataManager.GameMode.Banana );
		Application.LoadLevel( "main" );
	}

	public void Button_Quit()
	{
		SaveDataManager.SetGameMode( SaveDataManager.GameMode.Dummy);
		Application.LoadLevel( "Title" );
	}


}
