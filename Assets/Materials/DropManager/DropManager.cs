using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoxyLib;

public class DropManager : MonoBehaviour
{

	public GameObject Drop_GameObject;

	private const float BananaRadius = 1.8f;

	List<Drop> allDrops = new List<Drop>();
	List<Drop> linkedDrops = new List<Drop>();

	// Use this for initialization
	void Start()
	{

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

		if( TouchUtil.GetTouch() == TouchUtil.TouchInfo.Ended )
		{

			if( linkedDrops.Count == 0 ) return;

			//最初にlinkしたドロップで分け．バナナならバナナがひとつだけ入ってる．
			switch( linkedDrops[0].GetDropType() )
			{
			case DropType.BANANA1:
			case DropType.BANANA2:
			case DropType.BANANA3:
				ExplodeBanana( linkedDrops[0] );
				linkedDrops.Clear();
				break;

			case DropType.GO:

				while( linkedDrops.Count % 3 != 0 )
				{
					linkedDrops[linkedDrops.Count - 1].UnLink();
					linkedDrops.RemoveAt( linkedDrops.Count - 1 );
				}
				if( linkedDrops.Count == 0 ) return;
				foreach( Drop drop in linkedDrops ) drop.Erase();

				//ゴリラゴリラ消しならドロップひとつが1バナナになる
				if( linkedDrops.Count == 6 ) linkedDrops[0].SetDropType( DropType.BANANA1 );
				//ゴリラゴリラゴリラ消しならドロップひとつが3バナナになる
				if( linkedDrops.Count >= 9 ) linkedDrops[0].SetDropType( DropType.BANANA3 );
				linkedDrops.Clear();

				break;
			}

		}

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
		}
		BananaDrop.ExplodeBanana();
	}

	public void ClearAll()
	{
		foreach( Drop drop in allDrops ) drop.Erase();
	}

}
