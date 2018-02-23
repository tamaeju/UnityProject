using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Threading.Tasks;

namespace makeCSVclass {


	class makeCSV {
		public int MaxtileCount = 30;
		public mapObject[,] maptileobject;

		void preUseScript() {
			maptileobject = new mapObject[MaxtileCount, MaxtileCount];

		}
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
					sw.WriteLine("{0},{1},{2}", i, j, maptileobject[i, j].returnThisState());
				}
			}
			sw.Flush();
			sw.Close();
		}


	}
	class mapObject {
		state mystate = state.road;

		//ステイトをチェンジさせるクラス、値を入れると、それに対応したenumに設定される
		public void changeState(int statenum) {
			mystate = (state)Enum.ToObject(typeof(state), statenum);
		}

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

