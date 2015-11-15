using UnityEngine;
using System.Collections;

public class StageSelectManager : MonoBehaviour {

	public GameObject Drop_GameObject;

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
