using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditator : MonoBehaviour { //オブジェクトの参照を代替わりするクラス

	[SerializeField]
	CSVManager csvmanager;

	[SerializeField]
	MakeManager makemanager;
	[SerializeField]
	DataPathManager datapathmanager;
	[SerializeField]
	ItemmakeEditorCreater UIdraghmanager;
	[SerializeField]
	TouchEventManager touchmanager;
	[SerializeField]
	MapEditorUIManager UImanager;
	[SerializeField]
	PrefabContainer prefabcontainer;
	[SerializeField]
	DataCheck datachecker;
	[SerializeField]
	MassDealer massdealer;
	[SerializeField]
	DataChangerFromJaG jagchanger;
	[SerializeField]
	ButtonEventManager buttonmanager;
	[SerializeField]
	ItemMakerCreater itemmakermanager;
	[SerializeField]
	ClearConditionManager clearmanager;

	[SerializeField]
	DataCreateScene datascene;
	[SerializeField]
	DataStorage dataholder;
	[SerializeField]
	MapDataManager mapdatamanager;

	public CSVManager getcsvmanager () {
		return csvmanager;
	}

	public DataPathManager getdatapathmanager () {
		return datapathmanager;
	}
	public ItemmakeEditorCreater getUIdraghmanager () {
		return UIdraghmanager;
	}
	public MakeManager getmakemanager () {
		return makemanager;
	}
	public TouchEventManager gettouchmanager () {
		return touchmanager;
	}
	public MapEditorUIManager getUImanager () {
		return UImanager;
	}
	public PrefabContainer getprefabcontainer () {
		return prefabcontainer;
	}
	public DataCheck getdatachecker () {
		return datachecker;
	}
	public MassDealer getmassdealer () {
		return massdealer;
	}
	public DataChangerFromJaG getjagchanger () {
		return jagchanger;
	}
	public ButtonEventManager getbuttonmanager () {
		return buttonmanager;
	}
	public ItemMakerCreater getitemmakermanager () {
		return itemmakermanager;
	}
	public ClearConditionManager getclearmanager () {
		return clearmanager;
	}

	public DataCreateScene getdatascene () {
		return datascene;
	}
	public DataStorage getdataholder () {
		return dataholder;
	}
	public MapDataManager getmapdatamanager () {
		return mapdatamanager;
	}
}