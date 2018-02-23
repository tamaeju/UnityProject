using UnityEngine;
using System.Collections;
using System.IO;

public class MakeCsv : MonoBehaviour {

	class makeCSV {
		public int MaxtileCount = 30;
		public mapObject[,] maptileobject;

		void preusethisscript() {
			maptileobject = new mapObject[MaxtileCount, MaxtileCount];

		}
		void pushMakeCsvButton() {
			logSave();
		}
		public void logSave() {
			StreamWriter sw;
			FileInfo fi;
			string ApplicationdataPath = "";
			fi = new FileInfo(ApplicationdataPath + "/FileName.csv");
			sw = fi.AppendText();
			for (int i = 0; i < MaxtileCount; i++) {
				for (int j = 0; j < MaxtileCount; j++) {
					sw.WriteLine("{0},{1},{2}", i, j, maptileobject[i, j].returnThisState());
				}
			}
			sw.Flush();
			sw.Close();
		}


	}
	class mapObject {
		state mystate = state.road;


		public int returnThisState() {
			return (int)mystate;
		}
		public enum state {
			road,
			block,
			enemy,
			wall

		}
	}
}

