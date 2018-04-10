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

	public void pushStartButton() {
		objectmaker.LoadMapDatas();
		currentdataholder.GetClearConditionData();
		objectmaker.instanciateAllMapObject();
		movedealer.LoadFieldObject();
	}

}
