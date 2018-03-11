using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragButtonManager : MonoBehaviour {
	//UIのドラッグボタンを管理するクラス。
	//それぞれのUIドラッグボタンをインスタンシエイトし、それらのセーブボタンを押した時に、そのオブジェクトに対応した値を変更し、最終的にデータオブジェクトに返す。
	GameObject[] UIDragButton;
	GameObject[] UIDragButtonskind;
	DataManager datamanager;

	[SerializeField]
	GameObject UIButtonPrefab;
	

	// Use this for initialization
	void Start () {
		Vector3 instancepos = new Vector3();
		instancepos = UIButtonPrefab.transform.position;
		UIDragButton[0] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity) as GameObject;
		instancepos.y = instancepos.y - 200;
		UIDragButton[1] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity) as GameObject;
		instancepos.y = instancepos.y - 200;
		UIDragButton[2] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity) as GameObject;
	}
	//データマネージャーにアクセスして、自身の情報を全て渡すメソッド
	//セーブボタンを押された時に、自身の持つ情報をデータマネージャーに伝える。のだが、どういう形で渡すかというのが問題となってくる。
	//データマネージャー自体がすべての情報を更新する形にするのかといとまた違う気がするのだが、
	void onclickSaveButton() {

	}
	

}
