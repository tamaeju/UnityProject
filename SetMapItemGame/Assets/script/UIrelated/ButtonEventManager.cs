using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventManager : MonoBehaviour {//ボタンを押した時のクリック処理の大方を扱うクラス。
	[SerializeField]
	Meditator meditator;
	Action makemethod;
	Action<int> makeMakermethod;
	Action<Dropdown> changecsvmethod;
	Action makecsvmethod;
	Action makeobjectmakeitemmakerdeleteUImethod;



	public void makeMapCsvButton() {//UImanagerのデータを取得し、レベルデザインデータへ反映した後、csvmanagerにセーブ要求
		makecsvmethod();
	}

	public void setmakeMapCsvButton(Action anact) {//UImanagerのデータを取得し、レベルデザインデータへ反映した後、csvmanagerにセーブ要求
		makecsvmethod = anact;
	}



	public void makeObjectButton() {//csvmanaにデータをとってくるよう要求、そのデータを用いて、makemaneにインスタンス生成を要求、そのデータにて、置けるか置けないかを上書き、プレイヤーのagerntをゴールに設定。
											  //itemデータもその後取得し、datamanagerへそのデータ更新要求を行って、そのデータからアイテムメイカーというオブジェクト作成依頼をかける。
		makeobjectmakeitemmakerdeleteUImethod();
	}

	public void setmakeObjectButton(Action anact) {
		makeobjectmakeitemmakerdeleteUImethod = anact;
	}


	public void ChangeCSVNum(Dropdown dropdown) {//保存先と、呼び出し先のcsvを変更するメソッド、マップデータ以外は１つのcsvファイルに保存（インデックスがステージになっているのでそう設計した）
		changecsvmethod(dropdown);
	}

	public void setChangeCSVNum(Action<Dropdown> anact) {//保存先と、呼び出し先のcsvを変更するメソッド、マップデータ以外は１つのcsvファイルに保存（インデックスがステージになっているのでそう設計した）
		changecsvmethod = anact;
	}



	public void makeItemMaker(int stageNum) {
		makeMakermethod(stageNum);
	}

	public void setmakeItemMaker(Action<int> anact) {
		makeMakermethod = anact;
	}



	public void CanvasOFFButton() {//キャンバスの表示オフ
		Transform trasnform = meditator.getprefabcontainer().getmapmassuipositionObject().GetComponent<Transform>();
		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(false);
		}
	}
	public void CanvasONButton() {//キャンバスの表示オン
		Transform trasnform = meditator.getprefabcontainer().getmapmassuipositionObject().GetComponent<Transform>();
		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(true);
		}
	}
	public void callloadMapCSV() {//指定のcsvからデータを読み込み、UIでのボタンのstateを変える。
		MapEditorUIManager UImanager = meditator.getUImanager();
		UImanager.loadMapCSV();
	}
}

