using UnityEngine;
using System.Collections;

public class makeObject : MonoBehaviour {//オブジェクト生成を行うクラス。


	int[,] dataelements;
	GameObject [,] dataobjects;
	Vector3 instanciatePos;
	public GameObject[] settingprefab = new GameObject[4];
	public RayEmit rayemitter;
	float blocklength = 0.9f;
	private GameObject goalobject;
	private GameObject playerobject;

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
		}
	}

	public void instanciateAllObject(int[,] aPrefabKind,float instanciatehight) {
		for (int j = 0; j < aPrefabKind.GetLength(1); ++j) {
			for (int i = 0; i < aPrefabKind.GetLength(0); ++i) {
				if (aPrefabKind[i, j] == 0) {
				}
				else if (aPrefabKind[i, j] == 1) {
					Instantiate(settingprefab[aPrefabKind[i, j]], settingObjectPos(i, j, instanciatehight), Quaternion.identity);
				}
				else if (aPrefabKind[i, j] == 2) {//プレイヤーオブジェクトを生成する時はプレイヤーオブジェクトの参照を保持
					playerobject = Instantiate(settingprefab[aPrefabKind[i, j]], settingObjectPos(i, j, instanciatehight), Quaternion.identity) as GameObject;
				}
				else if (aPrefabKind[i, j] == 3) {//ゴールオブジェクトを生成する時はゴールオブジェクトの参照を保持
					goalobject = Instantiate(settingprefab[aPrefabKind[i, j]], settingObjectPos(i, j, instanciatehight), Quaternion.identity) as GameObject;
				}

			}
		}
	}
	public GameObject getPlayerObject() {
		return playerobject;
	}
	public GameObject getGoalObject() {
		return goalobject;
	}

	//メイクオブジェクトがオブジェクト群を生成。その後ゲームスタートボタンを押すとレベルデザインクリエイトクラスが、オブジェクトたちにゴールオブジェクトの場所を渡す。

	void instanciateObject(Vector3 pos, int i) {
			Instantiate(settingprefab[i], pos, Quaternion.identity);
	}


	Vector3 settingObjectPos(int x, int y,float z)
	{
		Vector3 returnPos = new Vector3((x - 4.5f) * blocklength, z, (y - 4.5f) * blocklength);
		return returnPos;
	}
}

