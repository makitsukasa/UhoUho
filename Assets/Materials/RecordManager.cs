using UnityEngine;
using System.Collections;

public class RecordManager : MonoBehaviour {

	public GameObject Drop_GameObject;

	// Use this for initialization
	void Start () {

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

	public void Button_DeleteAll()
	{
		SaveDataManager.DeleteAll();
		Application.LoadLevel( "Title" );
	}

	public void Button_Title()
	{
		Application.LoadLevel( "Title" );
	}

	public void Button_AzukiFont()
	{
		Application.OpenURL( "http://azukifont.com/" );
	}

	public void Button_AuthorTwitter()
	{
		Application.OpenURL( "https://twitter.com/tsukasa_boxy/" );
	}


}
