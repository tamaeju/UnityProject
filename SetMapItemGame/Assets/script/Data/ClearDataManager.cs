using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ClearDataManager : MonoBehaviour {
	DataChangerFromJaG jagchanger;
	CSVManager csvmanager;
	DataPathManager datapathmanager;
	[SerializeField]
	Meditator meditator;

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