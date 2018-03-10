using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CharactorMove {
	public Animation anim;

	void Start () {
		anim = this.gameObject.GetComponent<Animation>();
		anim.Play();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
