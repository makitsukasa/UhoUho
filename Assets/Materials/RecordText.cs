using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordText : MonoBehaviour {

	Text Text;
	SaveDataManager SaveDataManager;

	// Use this for initialization
	public void Start () {

		Text = this.gameObject.GetComponent<Text>();
		SaveDataManager = GameObject.Find( "SaveDataManager" ).GetComponent<SaveDataManager>();
		//Debug.Log( this.name );

		switch( this.name )
		{
		case "TotalChar":
			Text.text = "いままでに消した文字数 " + SaveDataManager.Get( SaveDataManager.Key.CharNum );
			break;

		case "TotalGorilla1":
			Text.text = "いままでに消したゴリラの数 " 
				+ SaveDataManager.Get( SaveDataManager.Key.Gorilla1Num );
			break;

		case "TotalGorilla2":
			Text.text = "いままでに消したゴリラゴリラの数 " 
				+ SaveDataManager.Get( SaveDataManager.Key.Gorilla2Num );
			break;

		case "TotalGorilla3":
			Text.text = "いままでに消した\nゴリラゴリラゴリラの数 " 
				+ SaveDataManager.Get( SaveDataManager.Key.Gorilla3Num );
			break;

		case "TotalGorilla4":
			Text.text = "いままでに消した\nゴリラゴリラゴリラゴリラの数 " 
				+ SaveDataManager.Get( SaveDataManager.Key.Gorilla4Num );
			break;

		case "TotalGorilla5":
			Text.text = "いままでに消した\nゴリラゴリラゴリラゴリラゴリラ以上の数 " 
				+ SaveDataManager.Get( SaveDataManager.Key.Gorilla5Num );
			break;

		case "TotalBanana":
			Text.text = "いままでに消したバナナの数 " + SaveDataManager.Get( SaveDataManager.Key.BananaNum ) + "バナナ";
			break;

		case "TotalPlayNum":
			Text.text = "いままでのプレイ回数 " + SaveDataManager.Get( SaveDataManager.Key.PlayNum ) + "回";
			break;

		case "TotalPlayTime":
			Text.text = "いままでのプレイ時間 " + SaveDataManager.Get( SaveDataManager.Key.PlayTime ) + "秒";
			break;

		case "HighScoreNormal":
			Text.text = "とにかくゴリラのハイスコア " +
				SaveDataManager.GetHighScoreString( SaveDataManager.GameMode.Normal );
			break;

		case "HighScoreChars":
			Text.text = "100ゴリラのハイスコア " +
				SaveDataManager.GetHighScoreString( SaveDataManager.GameMode.Chars );
				
			break;

		case "HighScoreBanana":
			Text.text = "バナナマスターのハイスコア " +
				SaveDataManager.GetHighScoreString( SaveDataManager.GameMode.Banana );
			break;

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
