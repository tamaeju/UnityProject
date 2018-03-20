using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	[SerializeField]
	itemstate mystate;
	public GameObject effect;
	state mystateinterface;
	state[] stateallay;

	void Start() {
		stateallay = new state[Config.itemkindlength];
		stateallay[(int)itemstate.faster] = new faster(this);
		stateallay[(int)itemstate.faster2] = new faster2(this);
		stateallay[(int)itemstate.slowdown] = new slowdown(this); 
		stateallay[(int)itemstate.slowdown2] = new slowdown2(this);
		stateallay[(int)itemstate.stop] = new stop(this);
		stateallay[(int)itemstate.stop2] = new stop2(this);
		stateallay[(int)itemstate.stop2] = new stop2(this);
		stateallay[(int)itemstate.stop2] = new stop2(this);
		stateallay[(int)mystate].changeColour();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			stateallay[(int)mystate].oncloisionobject(other);
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

	public void changeMyState(int stateNum) {
		mystate = (itemstate)stateNum;
	}

	abstract class state {
		abstract public void changeColour();
		abstract public void oncloisionobject(Collider other);
	}

	class faster : state {
		Item myitem;
		public faster(Item anItem) {
			myitem = anItem;
		}
		public override void changeColour() { myitem.GetComponent<Renderer>().material.color = Color.red; }
		public override void oncloisionobject(Collider other) {
			other.GetComponent<CharactorMove>().changeSpeed(5f, 5f);
			Instantiate(myitem.effect, myitem.transform.position, myitem.effect.transform.rotation);
			Destroy(myitem.gameObject);
		}
	}

	class slowdown : state {
		Item myitem;
		public slowdown(Item anItem) {
			myitem = anItem;
		}
		public void changeColour() { myitem.GetComponent<Renderer>().material.color = Color.blue; }
		public void oncloisionobject(Collider other) {
			other.GetComponent<CharactorMove>().changeSpeed(0.5f, 3f);
			Instantiate(myitem.effect, myitem.transform.position, myitem.effect.transform.rotation);
			Destroy(myitem.gameObject);
		}
	}
	class stop : state {
		Item myitem;
		public stop(Item anItem) {
			myitem = anItem;
		}
		public void changeColour() { myitem.GetComponent<Renderer>().material.color = Color.green; }
		public void oncloisionobject(Collider other) {
			other.GetComponent<CharactorMove>().changeSpeed(0f, 2f);
			Instantiate(myitem.effect);
			Destroy(myitem.gameObject);
		}
	}

	class faster2 : state {
		Item myitem;
		public faster2(Item anItem) {
			myitem = anItem;
		}
		public void changeColour() { myitem.GetComponent<Renderer>().material.color = Color.yellow; }
		public void oncloisionobject(Collider other) {
			other.GetComponent<CharactorMove>().changeSpeed(10, 2);
			Instantiate(myitem.effect, myitem.transform.position, myitem.effect.transform.rotation);
			Destroy(myitem.gameObject);
		}
	}

	class slowdown2 : state {
		Item myitem;
		public slowdown2(Item anItem) {
			myitem = anItem;
		}
		public void changeColour() { myitem.GetComponent<Renderer>().material.color = Color.cyan; }
		public void oncloisionobject(Collider other) {
			other.GetComponent<CharactorMove>().changeSpeed(0.3f, 3f);
			Instantiate(myitem.effect, myitem.transform.position, myitem.effect.transform.rotation);
			Destroy(myitem.gameObject);
		}
	}
	class stop2 : state {
		Item myitem;
		public stop2(Item anItem) {
			myitem = anItem;
		}
		public void changeColour() { myitem.GetComponent<Renderer>().material.color = Color.grey; }
		public void oncloisionobject(Collider other) {
			other.GetComponent<CharactorMove>().changeSpeed(0f, 4f);
			Instantiate(myitem.effect, myitem.transform.position, myitem.effect.transform.rotation);
			Destroy(myitem.gameObject);
		}
	}
	
}
