using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	[SerializeField]
	private GameObject scoreObject;
	private Text scoreText;
	private int scorecount;
	void Start() {
		scoreText = scoreObject.GetComponent<Text>();
		scorecount = 0;
		scoreText.text = scorecount.ToString();
	}

	void scoreUP () {
		scorecount++;
		scoreText.text = scorecount.ToString();
	}
}
