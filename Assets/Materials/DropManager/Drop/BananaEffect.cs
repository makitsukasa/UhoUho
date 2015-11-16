using UnityEngine;
using System.Collections;

public class BananaEffect : MonoBehaviour {

	ParticleSystem ParticleSystem;

	// Use this for initialization
	void Start () {
		ParticleSystem = this.GetComponent<ParticleSystem>();
		ParticleSystem.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if( ParticleSystem.isStopped )
		{
			Destroy( this.gameObject );
		}
	}

}
