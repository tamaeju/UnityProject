using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ClearDataManager : MonoBehaviour {
	clearconditiondata[] clearconditionaldatas; //clearconditiondata構造体の配列※ステージ数がインデックス
	DataChangerFromJaG jagchanger;
	CSVManager csvmanager;
	DataPathManager datapathmanager;
	[SerializeField]
	Meditator meditator;

	void Start () { //各データの初期化
		datapathmanager = meditator.getdatapathmanager ();
		jagchanger = meditator.getjagchanger ();
		csvmanager = meditator.getcsvmanager ();
	}
	public void LoadALLclearconditondata () { //csvデータを読み込んできてアイテムデータを上書き。
		clearconditionaldatas = csvmanager.getCCDataElement ();
	}
	// public clearconditiondata getStageClearCondition () {
	// 	LoadALLclearconditondata ();
	// 	MapDataManager mapdatamanager = meditator.getmapdatamanager ();
	// 	return clearconditionaldatas[mapdatamanager.getStageNum ()];
	// }
	// public clearconditiondata[] getclearconditondata () { //csvデータを読み込んできてアイテムデータを上書き。
	// 	LoadALLclearconditondata ();
	// 	return clearconditionaldatas;
	// }

}