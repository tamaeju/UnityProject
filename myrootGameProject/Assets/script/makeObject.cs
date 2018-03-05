using UnityEngine;
using System.Collections;

public class makeObject : MonoBehaviour {//オブジェクト生成を行うクラス。


	int[,] dataElements;
	GameObject [,] dataObject;
	Vector3 instanciatePos;
	public GameObject[] settingprefab = new GameObject[4];
	public RayEmit rayemitter;




	void Update() {
		if (Input.GetMouseButtonDown(0)) {
		}

	}

	public void instanciateAllObject(int[,]aPrefabKind) {
		for (int j = 0; j < aPrefabKind.GetLength(1); ++j) {
			for (int i = 0; i < aPrefabKind.GetLength(0); ++i) {
				if (aPrefabKind[i, j] == 0) {
				}
				else {
					instanciateObject(settingPosition(i, j, 0), aPrefabKind[i, j]);
				}
				
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
	Vector3 settingPosition(int x, int y,int z)
	{
		Vector3 returnPos = new Vector3((x - 4) * 0.9f, z, (y - 5) * 0.9f);
		return returnPos;
	}
}

