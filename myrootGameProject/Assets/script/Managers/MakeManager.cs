using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class MakeManager : MonoBehaviour {//オブジェクト生成を行うクラス。
	
	float blocklength = Config.blocklength;
	private GameObject goalobject;
	private GameObject playerobject;
	private int slidespace = 4;
	float groundhight;
	float instancehight;

	[SerializeField]
	Meditator meditator;

	[SerializeField]
	PrefabContainer objectcontainer;

	void Start() {
		groundhight = objectcontainer.getground().transform.position.y;
		instancehight = groundhight + 0.5f;
	}


	void Update() {
		if (Input.GetMouseButtonDown(0)) {
		}
	}

	public void instanciateAllObject(int[,] aPrefabKind) {
		GameObject[] instanceObjects = objectcontainer.getinstanceObjects();
		GameObject popobject;
		for (int j = 0; j < aPrefabKind.GetLength(1); ++j) {
			for (int i = 0; i < aPrefabKind.GetLength(0); ++i) {
				if (aPrefabKind[i, j] != 0) {//0はアイテムなし
					popobject = Instantiate(instanceObjects[aPrefabKind[i, j]], settingObjectPos(i, j, instancehight), Quaternion.identity) as GameObject;
					if (popobject.GetComponent<PlayerMove>()) {
						playerobject = popobject;
					}
					else if (popobject.GetComponent<Goal>()) {
						goalobject = popobject;
					}
					else if (popobject.GetComponent<TargetMove>()) {
						popobject.GetComponent<TargetMove>().getclearconditioner(meditator.getclearmanager());
					}
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


	Vector3 settingObjectPos(int x, int y,float z)
	{
		Vector3 returnPos = new Vector3(x * blocklength, z, y * blocklength);
		return returnPos;
	}
	public float getObjecthight() {
		return instancehight;
	}


	public GameObject InstanciateandGetRef(int onjectindex,Vector3 instancepos) {
		GameObject objectref;
		GameObject[] instanceObjects = objectcontainer.getitemObjects();

		objectref = Instantiate(instanceObjects[onjectindex], instancepos, Quaternion.identity) as GameObject;
		return objectref;

	}

	public GameObject MakeGetUIobject(GameObject instanceprefab, Vector2 objectpos) {//vector2の引数を与えれば、キャンバスのトランスフォーム+引数の場所にオブジェクトを生成し、そのオブジェクトの参照を返してくれるメソッド。
		Transform canvastrans = objectcontainer.getcanvasposition().transform;
		GameObject getobject = Instantiate(instanceprefab, this.transform.position, Quaternion.identity, canvastrans) as GameObject;
		getobject.transform.position = new Vector3(canvastrans.position.x + objectpos.x, canvastrans.position.y + objectpos.y, this.transform.position.z);
		return getobject;
	}
	//if (aPrefabKind[i, j] == 0) {
	//}
	//else if (aPrefabKind[i, j] == 1) {
	//	Instantiate(instanceObjects[aPrefabKind[i, j]], settingObjectPos(i, j, instancehight), Quaternion.identity);
	//}
	//else if (aPrefabKind[i, j] == 2) {//プレイヤーオブジェクトを生成する時はプレイヤーオブジェクトの参照を保持
	//	playerobject = Instantiate(instanceObjects[aPrefabKind[i, j]], settingObjectPos(i, j, instancehight), Quaternion.identity) as GameObject;
	//}
	//else if (aPrefabKind[i, j] == 3) {//ゴールオブジェクトを生成する時はゴールオブジェクトの参照を保持
	//	goalobject = Instantiate(instanceObjects[aPrefabKind[i, j]], settingObjectPos(i, j, instancehight), Quaternion.identity) as GameObject;
	//}
	//playerムーブコンポーネントをもってたらplayerに代入的な。
	//ポップオブジェクトがプレイヤーコンポーネントを持っていればplayerオブジェクトを代入する
}

