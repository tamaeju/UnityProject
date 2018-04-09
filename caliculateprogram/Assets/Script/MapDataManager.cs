using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class MapDataManager : MonoBehaviour {
	MassStruct[,] massdata;

	public void SetAllMassData(MassStruct[,] gotmassdata) {
		massdata = gotmassdata;
	}

	public  int GetMassNum(Vector2 checkpos) {
		return massdata[(int)checkpos.x, (int)checkpos.y].massnumber;
	}
	public int GetMassKind(Vector2 checkpos) {
		return massdata[(int)checkpos.x, (int)checkpos.y].masskind;
	}
}
