using UnityEngine;
using System.Collections;

public class LinkLineEffect : MonoBehaviour {

	bool flag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( flag )
		{
			Destroy( this.gameObject );
		}
		else
		{
			flag = true;
		}
	}
}
