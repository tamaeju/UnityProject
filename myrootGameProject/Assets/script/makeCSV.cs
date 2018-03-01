using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class makeCSV {
	public int MaxtileCount = 10;
	public GameObject[,] maptileobject;

	void pushMakeCsvButton() {
		logSave();
	}
	public void logSave() {
		StreamWriter sw;
		FileInfo fi;
		string ApplicationdataPath = "";
		string FileName = "";
		fi = new FileInfo(ApplicationdataPath + FileName);
		sw = fi.AppendText();

		for (int j = 0; j < MaxtileCount; j++) {
			for (int i = 0; i < MaxtileCount; i++) {
				//Vector2 pos =  maptileobject[i, j].
				//sw.WriteLine("{0},{1},{2}", i, j, .returnThisState());
			}
		}
		sw.Flush();
		sw.Close();
	}

}

