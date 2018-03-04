using UnityEngine;
using System.Collections;

public class makeObject : MonoBehaviour {//オブジェクト生成を行うクラス。


	int[,] dataElements;
	GameObject [,] dataObject;
	Vector3 instanciatePos;
	public int blockKind = 4;
	public GameObject[] settingprefab = new GameObject[4];
	public RayEmit rayemitter;

	void Start() {
		//StartCoroutine("instanciateAllObject");
	}


	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			//instanciateObject(rayemitter.checkPos(),1);
		}

	}

	public void instanciateAllObject(int[,]aPrefabKind) {
		for (int j = 0; j < aPrefabKind.GetLength(1); ++j) {
			for (int i = 0; i < aPrefabKind.GetLength(0); ++i) {
				instanciateObject(settingPosition(i, j), aPrefabKind[i,j]);//まだバグってるはず
			}
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
				Debug.Log(dataElements[i,j]);
			}
			Debug.Log("一行下がり");
		}
	}
	Vector3 settingPosition(int x, int y)
	{
		Vector3 returnPos = new Vector3((x - 4) * 0.9f, (y - 5) * 0.9f, 0);
		return returnPos;

	}
}

