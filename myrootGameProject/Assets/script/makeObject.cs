using UnityEngine;
using System.Collections;

public class makeObject : MonoBehaviour {


	int[][] dataElements;
	mapObject[,] dataObject;
	Vector3 instanciatePos;
	public int[] blockKind = new int[4];
	public GameObject[] settingprefab = new GameObject[4];
	int YCount = 30;//テスト用
	int XCount = 30;//テスト用
	int stateFactoNnum = 3;
	public RayEmit rayemitter;
	void Start() {
		makeMapObject();
		StartCoroutine("instanciateAllObject");
	}

	void makeMapObject() {
		makeDataElement makeDataclass = new makeDataElement();
		dataElements = makeDataclass.getDataElement((@"C:\Users\appirits_1020520\Documents\myGameProject\myrootGameProject\Assets\data\datacsvFile.csv"));

		testANDcheckData();
	}
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			instanciateObject(rayemitter.checkPos(),1);
		}

	}

	private IEnumerator instanciateAllObject() {
		yield return new WaitForSeconds(1.0f);
		for (int j = 0; j < dataElements.GetLength(1); ++j) {
			instanciatePos.y = j;
			for (int i = 0; i < dataElements.GetLength(0); ++i) {
				instanciatePos.x = i;
				instanciateObject(instanciatePos, blockKind[2]);//まだバグってるはず
			}
			yield return null;
		}
	}

	void instanciateObject(Vector3 pos, int i) {
		if (pos != null) {
			Instantiate(settingprefab[i], pos, Quaternion.identity);
		}
		else { }
	}
	void testANDcheckData() {
		for (int j = 0; j < dataElements.GetLength(1); ++j) {
			for (int i = 0; i < dataElements.GetLength(0); ++i) {
				Debug.Log(dataElements[j][i]);
			}
			Debug.Log("一行下がり");
		}
	}
}

