using UnityEngine;
using System.Collections;

public class LevelDesignCreate : MonoBehaviour {

	int maxColumn = 10;
	public GameObject[] dataObject = new GameObject[100];
	int[,] LevelDesignData;
	void Start() {
		LevelDesignData = new int[maxColumn, maxColumn];
	}

	public void MakeDesignData() {
		for (int j = 0; j < maxColumn; ++j) {
			for (int i = 0; i < maxColumn; ++i) {
				LevelDesignData[i, j] = dataObject[j*10+i].GetComponent<LevelButton>().returnThisState();
			}
		}
		testShowDebug();
	}
	public void testShowDebug() {
		for (int j = 0; j < maxColumn; ++j) {
			for (int i = 0; i < maxColumn; ++i) {
				Debug.Log(LevelDesignData[i, j]);
			}
		}
	}
}
