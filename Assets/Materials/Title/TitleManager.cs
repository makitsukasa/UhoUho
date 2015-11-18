using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

	public GameObject Drop_GameObject;

	// Use this for initialization
	void Start ()
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
		if( Input.GetKeyUp( KeyCode.Escape ) ) Button_Quit();
	}

	public void Button_Start()
	{
		Application.LoadLevel( "StageSelect" );
	}

	public void Button_HowToPlay()
	{
		Application.LoadLevel( "HowToPlay" );
	}

	public void Button_Record()
	{
		Application.LoadLevel( "Record" );
	}

	public void Button_Quit()
	{
		Application.Quit();
	}

}
