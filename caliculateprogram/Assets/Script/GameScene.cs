using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class GameScene : MonoBehaviour {
	[SerializeField]
	FieldObjectMaker objectmaker;
	[SerializeField]
	CurrentStageData currentdataholder;
	[SerializeField]
	MassMoveDealer movedealer;
	[SerializeField]
	CSVManager csvmanager;
	[SerializeField]
	FieldObjectEditUICreator editUIcreator;
	[SerializeField]
	DataStorage dataholder;

	public void pushStartButton() {
		objectmaker.LoadMapDatas();
		currentdataholder.GetClearConditionData();
		objectmaker.instanciateAllMapObject();
		movedealer.LoadFieldObject();
	}

	public void SaveCurrentMapData() {//データホルダーにデータが入りきったらdataholderからとってくるように変更する。
		MassStruct[,] savedata = editUIcreator.getCurrentFieldDatas();
		csvmanager.MapCsvSave(savedata);
	}

	public void ChangeStagePathNum(Dropdown dropdown) {
		dataholder.ChangeStagePathNum(dropdown);
	}
	public void StrageLoadCsvData() {
		dataholder.LoadFromCSV();
	}
	public void deleteDebugUIEditor() {
		editUIcreator.deleteEditorUIbuttons();
	}
	public void LoadFieldEditorData() {
		dataholder.LoadFromCSV();
		editUIcreator.ButtonStatusUpdate(dataholder.GetMapDataElements());
	}

}

