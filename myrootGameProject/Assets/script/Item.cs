using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	[SerializeField]
	itemstate mystate;
	[SerializeField]
	GameObject effect;

	public void Start() {
		changeMyColour();
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (mystate == itemstate.faster) {
				other.GetComponent<CharactorMove>().changeSpeed(5, 5);
				Instantiate(effect, this.transform.position, effect.transform.rotation);
				Destroy(this.gameObject);
			}

			if (mystate == itemstate.slowdown) {
				other.GetComponent<CharactorMove>().changeSpeed(0.5f, 3f);
				Instantiate(effect, this.transform.position, effect.transform.rotation);
				Destroy(this.gameObject);
			}

			if (mystate == itemstate.stop) {
				other.GetComponent<CharactorMove>().changeSpeed(0f, 2f);
				Instantiate(effect, this.transform.position, effect.transform.rotation);
				Destroy(this.gameObject);
			}
			if (mystate == itemstate.faster2) {
				other.GetComponent<CharactorMove>().changeSpeed(10, 2);
				Instantiate(effect, this.transform.position, effect.transform.rotation);
				Destroy(this.gameObject);
			}

			if (mystate == itemstate.slowdown2) {
				other.GetComponent<CharactorMove>().changeSpeed(0.1f, 2f);
				Instantiate(effect, this.transform.position, effect.transform.rotation);
				Destroy(this.gameObject);
			}

			if (mystate == itemstate.stop2) {
				other.GetComponent<CharactorMove>().changeSpeed(0f, 4f);
				Instantiate(effect, this.transform.position, effect.transform.rotation);
				Destroy(this.gameObject);
			}
		}
	}


	public enum itemstate {
		faster,
		faster2,
		slowdown,
		slowdown2,
		stop,
		stop2,
		block,
		instanceblock
	}
	public void changeMyColour() {

		if ((int)mystate == 0)
			GetComponent<Renderer>().material.color = Color.red;
		if ((int)mystate == 1)
			GetComponent<Renderer>().material.color = Color.blue;
		if ((int)mystate == 2)
			GetComponent<Renderer>().material.color = Color.green;
		if ((int)mystate == 3)
			GetComponent<Renderer>().material.color = Color.yellow;
		if ((int)mystate == 4)
			GetComponent<Renderer>().material.color = Color.cyan;
		if ((int)mystate == 5)
			GetComponent<Renderer>().material.color = Color.grey;
		if ((int)mystate == 6)
			GetComponent<Renderer>().material.color = Color.magenta;
		if ((int)mystate == 7)
			GetComponent<Renderer>().material.color = Color.white;
	}
	public void changeMyState(int stateNum) {
		mystate = (itemstate)stateNum;
	}
}
