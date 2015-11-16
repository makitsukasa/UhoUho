using UnityEngine;
using System.Collections;
using BoxyLib;

public class Drop : MonoBehaviour
{

	private DropManager DropManager;
	ParticleSystem LinkEffect;
	Rigidbody2D Rigidbody2D;
	private SpriteRenderer SpriteRenderer;
	public GameObject BananaEffect_GameObject;
	public Sprite[] sprites = new Sprite[7];

	private const float TouchRadius = 0.4f;
	private const float LinkRadius = 2.0f;

	private bool flag_IsTouched = false;
	private float time_IsTouched;

	private DropType dropType;

	// Use this for initialization
	void Start()
	{

		DropManager = GameObject.Find( "DropManager" ).GetComponent<DropManager>();

		Rigidbody2D = this.GetComponent<Rigidbody2D>();

		LinkEffect = transform.FindChild( "LinkEffect" ).gameObject.GetComponent<ParticleSystem>();
		LinkEffect.Stop();

		SpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

		dropType = RandDropType();

		SpriteRenderer.sprite = sprites[(int)dropType];

		time_IsTouched = Time.time;

	}

	// Update is called once per frame
	void Update()
	{
		Update_Touch();
		Update_Draw();
		if( transform.position.y <= -2 )
		{
			Erase();
		}
	}

	void Update_Touch()
	{
		if( GetTouch() != TouchUtil.TouchInfo.Began ) return;

		if( DropManager.GetLastLinkedDrop() == null )
		{
			switch( this.dropType )
			{
			case DropType.GO:
			case DropType.BANANA1:
			case DropType.BANANA2:
			case DropType.BANANA3:
				DropManager.AddLinkedDrop( this );
				break;
			default:
				break;
			}
			return;
		}
		else if( DropManager.GetLastLinkedDrop().IsLinkable( this ) )
		{
			DropManager.AddLinkedDrop( this );
		}

	}

	void Update_Draw()
	{

		if( !LinkEffect.isPlaying && DropManager.IsLinked( this ) )
		{
			LinkEffect.Play();
			return;
		}

		switch( GetTouch() )
		{
		case TouchUtil.TouchInfo.Began:
			LinkEffect.Play();
			break;

		case TouchUtil.TouchInfo.Ended:
			LinkEffect.Stop();
			break;
		}
	}

	public bool IsLinkable( Drop to )
	{
		if( to == null ) return false;
		if( this.GetPosF().LengthSq( to.GetPosF() ) > LinkRadius ) return false;
		if( DropManager.IsLinked( to ) ) return false;

		if( ( this.dropType == DropType.GO && to.dropType == DropType.RI ) ||
			( this.dropType == DropType.RI && to.dropType == DropType.RA ) ||
			( this.dropType == DropType.RA && to.dropType == DropType.GO )    )
		{
			return true;
		}
		else return false;

	}

	public void Erase()
	{
		this.transform.position = new Vector3( Random.Range( -1.0f, 1.0f ), 9 + Random.Range( -0.5f, 0.5f ) );
		this.Rigidbody2D.velocity = Vector2.zero;
		flag_IsTouched = false;
		LinkEffect.Stop();
		InitDropType();
	}

	public void ExplodeBanana()
	{

		Instantiate( BananaEffect_GameObject, this.transform.position, new Quaternion() );

		switch( this.GetDropType() )
		{
		case DropType.BANANA1:
			this.Erase();
			break;
		case DropType.BANANA2:
			this.SetDropType( DropType.BANANA1 );
			LinkEffect.Stop();
			break;
		case DropType.BANANA3:
			this.SetDropType( DropType.BANANA2 );
			LinkEffect.Stop();
			break;
		}

	}

	public void UnLink()
	{
		LinkEffect.Stop();
	}

	public void InitDropType()
	{
		dropType = RandDropType();
		SpriteRenderer.sprite = sprites[(int)dropType];
	}

	/////////////////////////////////////////////////////////////////

	public void SetDropType( DropType type )
	{
		dropType = type;
		SpriteRenderer.sprite = sprites[(int)type];
	}

	public DropType GetDropType()
	{
		return dropType;
	}

	public bool IsBanana()
	{
		return	dropType == DropType.BANANA1 ||
				dropType == DropType.BANANA2 || 
				dropType == DropType.BANANA3;
    }

	public Vector2F GetPosF()
	{
		return new Vector2F( this.transform.position );
	}


	int RandDropType_Int()
	{
		return Random.Range( (int)DropType.GO, (int)DropType.RA + 1 );
	}

	DropType RandDropType()
	{
		return (DropType)RandDropType_Int();
	}

	TouchUtil.TouchInfo GetTouch()
	{
		Vector2F touchPos = new Vector2F( TouchUtil.GetTouchWorldPosition(Camera.main) );

		if( touchPos.IsInCircle( this.transform.position, TouchRadius ) && TouchUtil.GetTouch_Bool() )
		{
			if( flag_IsTouched )
			{
				return TouchUtil.TouchInfo.Stationary;
			}
			else
			{
				//フレームに１回だけ更新
				if( time_IsTouched != Time.time ) flag_IsTouched = true;
				return TouchUtil.TouchInfo.Began;
			}
		}
		else
		{
			if( flag_IsTouched )
			{
				//フレームに１回だけ更新
				if( time_IsTouched != Time.time ) flag_IsTouched = false;
				return TouchUtil.TouchInfo.Ended;
			}
			else
			{
				return TouchUtil.TouchInfo.None;
			}
		}

	}

}
