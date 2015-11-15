using UnityEngine;
using System.Collections;
using BoxyLib;

public class Drop_Title : MonoBehaviour
{
	
	Rigidbody2D Rigidbody2D;
	private SpriteRenderer SpriteRenderer;
	public Sprite[] sprites = new Sprite[7];

	private DropType dropType;

	// Use this for initialization
	void Start()
	{

		Rigidbody2D = this.GetComponent<Rigidbody2D>();

		SpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

		dropType = RandDropType();

		SpriteRenderer.sprite = sprites[(int)dropType];

	}

	// Update is called once per frame
	void Update()
	{
		if( transform.position.y <= -5.3 ) Erase();
	}

	public void Erase()
	{
		this.transform.position = new Vector3( Random.Range( -3.0f, 3.0f ), 5.3f, 100 );
		this.Rigidbody2D.velocity = new Vector2( 0, Random.Range( -10.0f, 10.0f ) );
		InitDropType();
	}

	public void InitDropType()
	{
		dropType = RandDropType();
		SpriteRenderer.sprite = sprites[(int)dropType];
	}

	/////////////////////////////////////////////////////////////////
	
	int RandDropType_Int()
	{
		return Random.Range( 1, 10 ) % 6;
	}

	DropType RandDropType()
	{
		return (DropType)RandDropType_Int();
	}
	
}