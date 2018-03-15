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
	ItemmakeEditorManager UIdraghmanager;
	[SerializeField]
	TouchEventManager touchmanager;
	[SerializeField]
	UIManager UImanager;
	[SerializeField]
	PrefabContainer objectcontainer;
	[SerializeField]
	DataCheck datachecker;
	[SerializeField]
	MassDealer massdealer;
	[SerializeField]
	DataChangerFromJaG jagchanger;

	public CSVManager getcsvmanager() {
		return csvmanager;
	}
	public DataManager getdatamanager() {
		return datamanager;
	}
	public DataPathManager getdatapathmanager() {
		return datapathmanager;
	}
	public ItemmakeEditorManager getUIdraghmanager() {
		return UIdraghmanager;
	}
	public MakeManager getmakemanager() {
		return makemanager;
	}
	public TouchEventManager gettouchmanager() {
		return touchmanager;
	}
	public UIManager getUImanager() {
		return UImanager;
	}
	public PrefabContainer getobjectcontainer() {
		return objectcontainer;
	}
	public DataCheck getdatachecker()
	{
		return datachecker;
	}
	public MassDealer getmassdealer()
	{
		return massdealer;
	}
	public DataChangerFromJaG getjagchanger()
	{
		return jagchanger;
	}
}
