using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using BoxyLib;

public class HowToPlayManager : MonoBehaviour
{

	public GameObject Drop_GameObject;
	private Text Subscription, NextButton_Text, PrevButton_Text;

	private const float BananaRadius = 1.8f;

	List<Drop_HowToPlay> allDrops = new List<Drop_HowToPlay>();
	List<Drop_HowToPlay> linkedDrops = new List<Drop_HowToPlay>();

	int currentPage = 0;
	const int SixCharPage = 3;
	const int LastPage = 6;

	string GetText( int page )
	{

		string[] texts =
		{
			"ゴリラをなぞって消すゲームだよ",
			"ゴ→リ→ラとなぞると消せるよ！\nやってみよう！",
			"ほら，消せたでしょ？",
			"ゴ→リ→ラ→ゴ→リ→ラ→…ってなぞってみよう",
			"バナナが出てきたね",
			"バナナをタッチすると周りも消えるよ！",
			"これで説明は終わり！早速やってみよう！",
		};

		return texts[page];
	}

	// Use this for initialization
	void Start()
	{

		Subscription = GameObject.Find( "Subscription" ).GetComponent<Text>();
		NextButton_Text = GameObject.Find( "NextButton_Text" ).GetComponent<Text>();
		PrevButton_Text = GameObject.Find( "PrevButton_Text" ).GetComponent<Text>();
		PrevButton_Text.text = "タイトルに戻る";
		NextButton_Text.text = "次のページへ";
		Subscription.text = GetText( currentPage );

		for( int i = 0; i <= 2; i++ )
		{
			Vector3 pos = new Vector3( i - 2.5f, 10 );
			GameObject hoge = Instantiate( Drop_GameObject, pos, new Quaternion() ) as GameObject;
			Drop_HowToPlay piyo = hoge.GetComponent<Drop_HowToPlay>();
			allDrops.Add( piyo );
			piyo.SetDropType_Start( (DropType)( i % 3 + 1 ) );
		}

	}

	// Update is called once per frame
	void Update()
	{

		if( TouchUtil.GetTouch() == TouchUtil.TouchInfo.Ended ||
			linkedDrops.Count > 0 && !TouchUtil.GetTouch_Bool() )
			Erase();

		if( Input.GetKeyUp( KeyCode.Escape ) ) Button_Prev();

	}


	public void Button_Next()
	{
		currentPage++;
		if( currentPage > LastPage )
		{
			Application_LoadLabel_main();
			return;
		}

		if( currentPage == 0 ) PrevButton_Text.text = "タイトルに戻る";
		else PrevButton_Text.text = "前のページへ";
		if( currentPage == 3 && allDrops.Count != 6 )
		{
			for( int i = 3; i <= 5; i++ )
			{
				Vector3 pos = new Vector3( i - 2.5f, 10 );
				GameObject hoge = Instantiate( Drop_GameObject, pos, new Quaternion() ) as GameObject;
				Drop_HowToPlay piyo = hoge.GetComponent<Drop_HowToPlay>();
				allDrops.Add( piyo );
				piyo.SetDropType_Start( (DropType)( i % 3 + 1 ) );
			}
		}
		if( currentPage == LastPage ) NextButton_Text.text = "やってみる！";
		else NextButton_Text.text = "次のページへ";

		Subscription.text = GetText( currentPage);
	}

	public void Button_Prev()
	{
		currentPage--;
		if( currentPage < 0 )
		{
			Application.LoadLevel( "Title" );
			return;
		}

		if( currentPage == 0 ) PrevButton_Text.text = "タイトルに戻る";
		else PrevButton_Text.text = "前のページへ";
		if( currentPage == LastPage ) NextButton_Text.text = "やってみる！";
		else NextButton_Text.text = "次のページへ";

		Subscription.text = GetText( currentPage );
	}

	void Application_LoadLabel_main()
	{
		SaveDataManager.SetGameMode( SaveDataManager.GameMode.Normal );
		Application.LoadLevel( "main" );
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

	private void Erase_Normal( List<Drop_HowToPlay> list )
	{

		//ゴリラゴなら最後のゴは排除
		while( list.Count % 3 != 0 )
		{
			list[list.Count - 1].UnLink();
			list.RemoveAt( list.Count - 1 );
		}

		if( list.Count == 0 ) return;

		foreach( Drop_HowToPlay drop in list )
		{
			drop.Erase();
		}

		//ゴリラゴリラ消しならドロップひとつが1バナナになる
		if( list.Count == 6 ) list[Random.Range( 0, 6 )].SetDropType( DropType.BANANA1 );
		//ゴリラゴリラゴリラ消しならドロップひとつが3バナナになる
		if( list.Count >= 9 ) list[Random.Range( 0, 6 )].SetDropType( DropType.BANANA3 );

	}

	public bool IsLinked( Drop_HowToPlay drop )
	{
		foreach( Drop_HowToPlay linkedDrop in linkedDrops )
		{
			if( linkedDrop == drop ) return true;
		}
		return false;
	}

	public void AddLinkedDrop( Drop_HowToPlay drop )
	{
		linkedDrops.Add( drop );
	}

	public Drop_HowToPlay GetLastLinkedDrop()
	{
		int num = linkedDrops.Count;
		if( num == 0 ) return null;
		return linkedDrops[linkedDrops.Count - 1];
	}

	private void ExplodeBanana( Drop_HowToPlay BananaDrop )
	{
		//バナナはバナナの周りを消す．バナナでバナナは消えない．
		foreach( Drop_HowToPlay drop in allDrops
			.Where( x => x.GetPosF().IsInCircle( BananaDrop.GetPosF(), BananaRadius ) )
			.Where( x => !x.IsBanana() ) )
		{
			drop.Erase();
		}
		BananaDrop.ExplodeBanana();
	}

	public void ClearAll()
	{
		foreach( Drop_HowToPlay drop in allDrops ) drop.Erase();
		/*
		Debug.Log( "PlayerPrefs CharNum" + SaveDataManager.Get( SaveDataManager.Key.CharNum ) + 
			", PlayTime" + SaveDataManager.Get(SaveDataManager.Key.PlayTime ) );
		*/
	}


}
