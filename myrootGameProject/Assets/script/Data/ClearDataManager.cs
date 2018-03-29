using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearDataManager : MonoBehaviour {
	clearconditiondata[] clearconditionaldatas;//clearconditiondata構造体の配列※ステージ数がインデックス
	DataChangerFromJaG jagchanger;
	CSVManager csvmanager;
	DataPathManager datapathmanager;
	[SerializeField]
	Meditator meditator;

	void Start() {//各データの初期化
		datapathmanager = meditator.getdatapathmanager();
		jagchanger = meditator.getjagchanger();
		csvmanager = meditator.getcsvmanager();

	}
	public void LoadALLclearconditondata() {//csvデータを読み込んできてアイテムデータを上書き。
		clearconditionaldatas = jagchanger.parsejagtodobleClearconditiondatas(csvmanager.getJagDataElement(datapathmanager.getconditiondatapath()));
	}
	public clearconditiondata getStageClearCondition(int stage) {
		LoadALLclearconditondata();
		return clearconditionaldatas[stage];
	}
	public clearconditiondata[] getclearconditondata() {//csvデータを読み込んできてアイテムデータを上書き。
		LoadALLclearconditondata();
		return clearconditionaldatas;
	}


}
