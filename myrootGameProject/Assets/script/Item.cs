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
		protected Color mycolor;
		protected float effecttime;
		protected float speed;
		protected Item myitem;

		public void changeColour() { myitem.GetComponent<Renderer>().material.color = mycolor; }
		public void oncloisionobject(Collider other) {
			other.GetComponent<CharactorMove>().changeSpeed(speed, effecttime);
			Instantiate(myitem.effect, myitem.transform.position, myitem.effect.transform.rotation);
			Destroy(myitem.gameObject);
		}
	}

	class faster : state {
		public faster(Item anItem) {
			myitem = anItem;
			effecttime = 5f;
			speed = 5f;
			mycolor = Color.magenta;
		}
	}
	class faster2 : state {
		public faster2(Item anItem) {
			myitem = anItem;
			speed = 10f;
			effecttime = 2f;
			mycolor = Color.red;
		}
	}
	class slowdown : state {
		public slowdown(Item anItem) {
			myitem = anItem;
			speed = 0.5f;
			effecttime = 3f;
			mycolor = Color.cyan;
		}
	}
	class slowdown2 : state {
		public slowdown2(Item anItem) {
			myitem = anItem;
			speed = 0.3f;
			effecttime = 3f;
			mycolor = Color.blue;
		}
	}
	class stop : state {
		public stop(Item anItem) {
			myitem = anItem;
			speed = 0f;
			effecttime = 2f;
			mycolor = Color.grey;
		}
	}



	class stop2 : state {
		public stop2(Item anItem) {
			myitem = anItem;
			speed = 0f;
			effecttime = 4f;
			mycolor = Color.black;
		}
	}

	
}
