using UnityEngine;
using System.Collections;

public class makeObjectFromCSV : MonoBehaviour {

	public GameObject[] settingprefab;
	int[][] dataElement;
	mapObject[,] dataObject;
	Vector3 instanciatePos;
	public float blockLength = 0.5f;
	public int blockKind = 1;
	int testYCount = 30;//テスト用
	int testXCount = 30;//テスト用
	int stateFactoNnum = 3;

	void makeMapObject() {
		dataElement = new int[testYCount][];
		for (int j = 0; j < testYCount; j++) {
			dataElement[j] = new int[testXCount];
		}
		
		dataObject = new mapObject[testXCount, testYCount];

		for (int j = 0; j < dataElement.GetLength(1); ++j) {
			for (int i = 0; i < dataElement.GetLength(0); ++i) {
				//このタイミングでオブジェクトをpointX、pointYの位置にインスタンシエイトする文を入れる　dataObject[i, j] = Instantiate(hogehogeprefab, new Vector3(i*10,j*10, 0), Quaternion.identity)asGameObject;
				dataObject[i, j].changeState(dataElement[j][stateFactoNnum]);
			}
		}
	}


	void instanciateAllObject() {
		for (int j = 0; j<dataElement.GetLength(1); ++j) {
			instanciatePos.y = j + blockLength;
			for (int i = 0; i<dataElement.GetLength(0); ++i) {
				instanciatePos.x = i + blockLength;
				instanciateObject(instanciatePos, blockKind);//まだバグってるはず
			}
		}
	}

	void instanciateObject(Vector3 pos ,int i) {
		Instantiate(settingprefab[i], pos, Quaternion.identity);
		}
	}
