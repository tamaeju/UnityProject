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
	GameObject goalobject;
	GameObject playerobject;
	int slidespace = 4;
	float groundhight;
	float instancehight;

	[SerializeField]
	Meditator meditator;

	[SerializeField]
	PrefabContainer objectcontainer;

	void Start() {//オブジェクトの生成位置の取得
		groundhight = objectcontainer.getground().transform.position.y;
		instancehight = groundhight + 0.5f;
	}


	public void instanciateAllMapObject(int[,] aPrefabKind) {//playerやブロックなどのオブジェクトを生成するメソッド。
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


	Vector3 settingObjectPos(int x, int y,float z)//i,jのインデックスからマップオブジェクトの生成位置を設定する処理。
	{
		Vector3 returnPos = new Vector3(x * blocklength, z, y * blocklength);
		return returnPos;
	}
	public float getObjecthight() {
		return instancehight;
	}

	public GameObject InstanciateandGetRef(int onjectindex,Vector3 instancepos) {//オブジェクトを生成して参照を返すメソッド
		GameObject objectref;
		GameObject[] instanceObjects = objectcontainer.getitemObjects();
		objectref = Instantiate(instanceObjects[onjectindex], instancepos, Quaternion.identity) as GameObject;
		return objectref;
	}



}

