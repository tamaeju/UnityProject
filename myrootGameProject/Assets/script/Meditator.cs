using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditator : MonoBehaviour {
//オブジェクトの参照を代替わりするクラス
	[SerializeField]
	CSVManager csvmanager;
	[SerializeField]
	DataManager datamanager;
	[SerializeField]
	MakeManager makemanager;
	[SerializeField]
	DataPathManager datapathmanager;
	[SerializeField]
	UIDragButtonManager UIdraghmanager;
	[SerializeField]
	TouchManager touchmanager;

	public CSVManager getcsvmanager() {
		return csvmanager;
	}
	public DataManager getdatamanager() {
		return datamanager;
	}
	public DataPathManager getdatapathmanager() {
		return datapathmanager;
	}
	public UIDragButtonManager getUIdraghmanager() {
		return UIdraghmanager;
	}
	public MakeManager getmakemanager() {
		return makemanager;
	}
	public TouchManager gettouchmanager() {
		return touchmanager;
	}
}
