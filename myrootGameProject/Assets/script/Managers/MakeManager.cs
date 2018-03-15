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
	ObjectContainer objectcontainer;

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

		for (int j = 0; j < aPrefabKind.GetLength(1); ++j) {
			for (int i = 0; i < aPrefabKind.GetLength(0); ++i) {
				if (aPrefabKind[i, j] == 0) {
				}
				else if (aPrefabKind[i, j] == 1) {
					Instantiate(instanceObjects[aPrefabKind[i, j]], settingObjectPos(i, j, instancehight), Quaternion.identity);
				}
				else if (aPrefabKind[i, j] == 2) {//プレイヤーオブジェクトを生成する時はプレイヤーオブジェクトの参照を保持
					playerobject = Instantiate(instanceObjects[aPrefabKind[i, j]], settingObjectPos(i, j, instancehight), Quaternion.identity) as GameObject;
				}
				else if (aPrefabKind[i, j] == 3) {//ゴールオブジェクトを生成する時はゴールオブジェクトの参照を保持
					goalobject = Instantiate(instanceObjects[aPrefabKind[i, j]], settingObjectPos(i, j, instancehight), Quaternion.identity) as GameObject;
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
	public float getBlockLength(){
		return blocklength;
	}

	public GameObject InstanciateandGetRef(int onjectindex,Vector3 instancepos) {
		GameObject objectref;
		GameObject[] instanceObjects = objectcontainer.getinstanceObjects();

		objectref = Instantiate(instanceObjects[onjectindex], instancepos, Quaternion.identity) as GameObject;
		return objectref;

	}
	public void makeDragedObjectandButton()
	{
		DataManager datamanager = meditator.getdatamanager();
		GameObject objectleftcount = objectcontainer.getobjectleftCount();
		GameObject[] instanceObjects = objectcontainer.getinstanceObjects();
		GameObject leftcountprefab = objectcontainer.getobjectleftCount();
		GameObject dragobjectmakerprefab = objectcontainer.getdragobjectmaker();

		for (int i = 0; i < Config.dragbuttonNum ; i++)
		{
			GameObject　itemleftCount = Instantiate(leftcountprefab, this.transform.position, Quaternion.identity) as GameObject;
			itemleftCount.transform.parent = objectcontainer.getcanvasposition().transform;
			//レフトカウントボタンを作成し、親オブジェクトをペアレントトランスフォームにしている。

			GameObject dragobjectmaker = Instantiate(dragobjectmakerprefab, dragobjectmakerprefab.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
			MakeDraggedObject draggedobject = dragobjectmaker.GetComponent<MakeDraggedObject>();
			//draggedobjectを作成し、コンポーネントの取得をしている。

			draggedobject.setREFofLeftCount(itemleftCount.GetComponent<Text>());
			//draggedobjectがleftcountを使用できるようにしている。


			draggedobject.setMyObjectKind(datamanager.getDragitemkind(i));
			draggedobject.setObjectLeftCount(datamanager.getDragitemleft(i));
			//draggedobjectのパラメータを設定している。

		//UI
		}
	}

}

