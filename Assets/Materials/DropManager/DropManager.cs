using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoxyLib;

public class DropManager : MonoBehaviour
{

	public GameObject Drop_GameObject;

	private SaveDataManager SaveDataManager;

	private const float BananaRadius = 1.8f;

	List<Drop> allDrops = new List<Drop>();
	List<Drop> linkedDrops = new List<Drop>();

	// Use this for initialization
	void Start()
	{

		SaveDataManager = GameObject.Find( "SaveDataManager" ).GetComponent<SaveDataManager>();

		for( int i = 0; i < 25; i++ )
		{
			Vector3 pos = new Vector3( Random.Range( -1.0f, 1.0f ), 10 + i * 0.4f, 0 );
			GameObject hoge = Instantiate( Drop_GameObject, pos, new Quaternion() ) as GameObject;
			allDrops.Add( hoge.GetComponent<Drop>() );
		}

	}

	// Update is called once per frame
	void Update()
	{

		if( TouchUtil.GetTouch() == TouchUtil.TouchInfo.Ended ) Erase();

	}

	public void Erase()
	{
		if( linkedDrops.Count == 0 ) return;

		//最初にlinkしたドロップで分け
		switch( linkedDrops[0].GetDropType() )
		{
		//最初のドロップがバナナならそれ以外入ってない
		case DropType.BANANA1:
		case DropType.BANANA2:
		case DropType.BANANA3:
			ExplodeBanana( linkedDrops[0] );
			linkedDrops.Clear();
			break;

		//最初のドロップがゴならゴリラゴリラゴリラ
		case DropType.GO:
			Erase_Normal( linkedDrops );
			linkedDrops.Clear();
			break;
		}

	}

	private void Erase_Normal( List<Drop> list )
	{

		//ゴリラゴなら最後のゴは排除
		while( list.Count % 3 != 0 )
		{
			list[list.Count - 1].UnLink();
			list.RemoveAt( list.Count - 1 );
		}

		if( list.Count == 0 ) return;

		foreach( Drop drop in list )
		{
			drop.Erase();
			SaveDataManager.Add( SaveDataManager.Key.CharNum );
		}

		switch( list.Count )
		{
		case 3:
			SaveDataManager.Add( SaveDataManager.Key.Gorilla1Num );
			break;
		case 6:
			SaveDataManager.Add( SaveDataManager.Key.Gorilla2Num );
			break;
		case 9:
			SaveDataManager.Add( SaveDataManager.Key.Gorilla3Num );
			break;
		case 12:
			SaveDataManager.Add( SaveDataManager.Key.Gorilla4Num );
			break;

		//over 15
		default:
			SaveDataManager.Add( SaveDataManager.Key.Gorilla5Num );
			break;
		}

		//ゴリラゴリラ消しならドロップひとつが1バナナになる
		if( list.Count == 6 ) list[0].SetDropType( DropType.BANANA1 );
		//ゴリラゴリラゴリラ消しならドロップひとつが3バナナになる
		if( list.Count >= 9 ) list[0].SetDropType( DropType.BANANA3 );

	}

	public bool IsLinked( Drop drop )
	{
		foreach( Drop linkedDrop in linkedDrops )
		{
			if( linkedDrop == drop ) return true;
		}
		return false;
	}

	public void AddLinkedDrop( Drop drop )
	{
		linkedDrops.Add( drop );
	}

	public Drop GetLastLinkedDrop()
	{
		int num = linkedDrops.Count;
		if( num == 0 ) return null;
		return linkedDrops[linkedDrops.Count - 1];
	}

	private void ExplodeBanana( Drop BananaDrop )
	{
		//バナナはバナナの周りを消す．バナナでバナナは消えない．
		foreach( Drop drop in allDrops
			.Where( x => x.GetPosF().IsInCircle( BananaDrop.GetPosF(), BananaRadius ) )
            .Where( x => !x.IsBanana() )												 )
        {
			drop.Erase();
			SaveDataManager.Add( SaveDataManager.Key.CharNum );
		}
		BananaDrop.ExplodeBanana();
		SaveDataManager.Add( SaveDataManager.Key.BananaNum );
	}

	public void ClearAll()
	{
		foreach( Drop drop in allDrops ) drop.Erase();
		/*
		Debug.Log( "PlayerPrefs CharNum" + SaveDataManager.Get( SaveDataManager.Key.CharNum ) + 
			", PlayTime" + SaveDataManager.Get(SaveDataManager.Key.PlayTime ) );
		*/
	}

	void OnDestroy()
	{
		SaveDataManager.Save();
		Debug.Log( "currentScore : " + SaveDataManager.GetCurrentScore() + Application.loadedLevelName );
	}

}
