using UnityEngine;
using System.Collections;
using BoxyLib;

public class Drop_Title : MonoBehaviour
{

	ParticleSystem Light;
	Rigidbody2D Rigidbody2D;
	private SpriteRenderer SpriteRenderer;
	public Sprite[] sprites = new Sprite[7];

	private const float TouchRadius = 0.4f;

	private bool flag_IsTouched = false;
	private float time_IsTouched = Time.time;

	private DropType dropType;

	// Use this for initialization
	void Start()
	{

		Rigidbody2D = this.GetComponent<Rigidbody2D>();

		Light = transform.FindChild( "Particle System" ).gameObject.GetComponent<ParticleSystem>();
		Light.Stop();

		SpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

		dropType = RandDropType();

		SpriteRenderer.sprite = sprites[(int)dropType];

	}

	// Update is called once per frame
	void Update()
	{
		Update_Touch();
		Update_Draw();
		if( transform.position.y <= -5.3 )
		{
			Erase();
		}
	}

	void Update_Touch()
	{
		if( GetTouch() != TouchUtil.TouchInfo.Began ) return;

	}

	void Update_Draw()
	{

		switch( GetTouch() )
		{
		case TouchUtil.TouchInfo.Began:
			Light.Play();
			break;

		case TouchUtil.TouchInfo.Ended:
			Light.Stop();
			break;
		}
	}
	
	public void Erase()
	{
		this.transform.position = new Vector3( Random.Range( -3.0f, 3.0f ), 5.3f );
		this.Rigidbody2D.velocity = new Vector2( 0, Random.Range( -10.0f, 10.0f ) );
		flag_IsTouched = false;
		Light.Stop();
		InitDropType();
	}

	public void UnLink()
	{
		Light.Stop();
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
		return dropType == DropType.BANANA1 ||
				dropType == DropType.BANANA2 ||
				dropType == DropType.BANANA3;
	}

	public Vector2F GetPosF()
	{
		return new Vector2F( this.transform.position );
	}


	int RandDropType_Int()
	{
		return Random.Range( 1, 10 ) % 6;
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