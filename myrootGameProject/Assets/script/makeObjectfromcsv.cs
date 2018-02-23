using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvReaderClass {

	class makeObjectfromDataElements {

		int[][] dataElement;
		mapObject[,] dataObject;

		int testYCount = 30;//テスト用
		int testXCount = 30;//テスト用
		int stateFactornum = 3;

		void makeMapObject() {

			dataElement = new int[testYCount][];
			for (int j = 0; j < testYCount; j++) {
				dataElement[j] = new int[testXCount];
			}


			dataObject = new mapObject[testXCount, testYCount];

			for (int j = 0; j < dataElement.GetLength(1); ++j) {
				for (int i = 0; i < dataElement.GetLength(0); ++i) {
					//このタイミングでオブジェクトをpointX、pointYの位置にインスタンシエイトする文を入れる　dataObject[i, j] = Instantiate(hogehogeprefab, new Vector3(i*10,j*10, 0), Quaternion.identity)asGameObject;
					dataObject[i, j].changeState(dataElement[j][stateFactornum]);
				}
			}
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
