using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
	public Vector3 pos;
	public int stateNum;

	public void chengeState(){
		if (stateNum < 3) {
			stateNum++;
		}
		else { stateNum = 0; }
		changeButtonColour();
	}
	void Start() {
		changeButtonColour();
	}

	public Vector2 returnThisPos() {
		return this.pos;
	}

	public int returnThisState() {
		return this.stateNum;
	}
	public void changeButtonColour() {
		if(stateNum == 0)
		gameObject.GetComponent<Image>().color = Color.red;
		if (stateNum == 1)
			gameObject.GetComponent<Image>().color = Color.blue;
		if (stateNum == 2)
			gameObject.GetComponent<Image>().color = Color.green;
		if (stateNum == 3)
			gameObject.GetComponent<Image>().color = Color.yellow;
	}
}
