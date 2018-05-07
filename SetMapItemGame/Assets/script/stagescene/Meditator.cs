using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditator : MonoBehaviour {//オブジェクトの参照を代替わりするクラス

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
	MapDataManager mapdatamanager;
	[SerializeField]
	ItemDataManager itemdatamanager;
	[SerializeField]
	ClearDataManager cleardatamanager;
	[SerializeField]
	DataCreateScene datascene;


	public CSVManager getcsvmanager() {
		return csvmanager;
	}
	public MapDataManager getmapdatamanager() {
		return mapdatamanager;
	}
	public DataPathManager getdatapathmanager() {
		return datapathmanager;
	}
	public ItemmakeEditorCreater getUIdraghmanager() {
		return UIdraghmanager;
	}
	public MakeManager getmakemanager() {
		return makemanager;
	}
	public TouchEventManager gettouchmanager() {
		return touchmanager;
	}
	public MapEditorUIManager getUImanager() {
		return UImanager;
	}
	public PrefabContainer getprefabcontainer() {
		return prefabcontainer;
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
	public ButtonEventManager getbuttonmanager() {
		return buttonmanager;
	}
	public ItemMakerCreater getitemmakermanager() {
		return itemmakermanager;
	}
	public ClearConditionManager getclearmanager() {
		return clearmanager;
	}
	public ItemDataManager getitemdatamanager() {
		return itemdatamanager;
	}
	public ClearDataManager getcleardatamanager() {
		return cleardatamanager;
	}
	public DataCreateScene getdatascene() {
		return datascene;
	}
}
