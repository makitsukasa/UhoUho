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
		PlayerPrefs.DeleteAll();

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
		HighScoreText_GorillaGorilla	= GameObject.Find( "HighScore_GorillaGorilla" )	.GetComponent<Text>();
		HighScoreText_Chars				= GameObject.Find( "HighScore_Chars" )			.GetComponent<Text>();
		HighScoreText_Banana			= GameObject.Find( "HighScore_Banana" )			.GetComponent<Text>();

		int highScore = PlayerPrefs.GetInt( "HighScoreNormal" );
		HighScoreText_Normal.text = "ハイスコア：\n" + highScore + "こ";
		highScore = PlayerPrefs.GetInt( "HighScoreGorillaGorilla" );
		HighScoreText_GorillaGorilla.text = "ハイスコア：\n" + highScore + "こ";
		highScore = PlayerPrefs.GetInt( "HighScoreChars", 999999999 );
		HighScoreText_Chars.text = "ハイスコア：\n" + highScore / 1000 + "\"" + ( highScore % 1000 ).ToString("000");
		highScore = PlayerPrefs.GetInt( "HighScoreBanana" );
		HighScoreText_Banana.text = "ハイスコア：\n" + highScore + "バナナ";

	}

	// Update is called once per frame
	void Update () {
	
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
