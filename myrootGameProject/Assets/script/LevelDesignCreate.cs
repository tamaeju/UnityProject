using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignCreate : MonoBehaviour
{//マップエディタのマネージャースクリプト。makeCSVクラスと、makeDataElementクラスを保有

	public static int maxColumn = 10;//他のクラスも参照する最大要素数
	public GameObject[] UIobjects = new GameObject[maxColumn * maxColumn];//インスペクタで代入するために
	int[,] _leveldesigndata;
	bool[,] _canSetDatas;
	public GameObject canvasObject;
	int loadColomn = 3;
	public GameObject _levelbutton;
	string filename;
	string datapath;
	private GameObject goalobject;
	private GameObject playerobject;
	[SerializeField]GameObject ground;
	private float groundhight;
	private float instancehight;
	float blocklength = 0.9f;

	void instanciateandGetUIObjects() {//インスペクターで紐づけを行うためのメソッド。インスタンシエイトしたタイミングでapplyして紐づけする。
		var parent = canvasObject.transform;
		for (int j = 0; j < maxColumn; ++j) {
			for (int i = 0; i < maxColumn; ++i) {
				UIobjects[j * 10 + i] = Instantiate(_levelbutton, setUIPos(i, j, 0), Quaternion.identity, parent) as GameObject;
				if (i == 0 | i == 9 | j == 0 | j == 9) {
					UIobjects[j * 10 + i].GetComponent<LevelButton>().changeState(1);
				}
			}
		}
	}
	Vector3 setUIPos(int x, int y, int z) {//InstanciateandgetREFmethod()と合わせ技のため
		Vector3 returnPos = new Vector3((x+10f)* 28, (y+3f)* 28 ,z);
		return returnPos;
	}

	void Start()//レベルデザインデータのメモリ領域確保
	{
		_leveldesigndata = new int[maxColumn, maxColumn];
		instanciateandGetUIObjects();
		filename = "testData.csv";
		datapath = Application.dataPath + "/data/" + filename;
		groundhight = ground.transform.position.y;
		instancehight = groundhight + 0.5f;
		_canSetDatas = new bool[maxColumn, maxColumn];
	}

	public void makeLevelDesignData()//レベルデザインデータを1次元配列から2次元配列へ置換
	{
		for (int j = 0; j < maxColumn; ++j)
		{
			for (int i = 0; i < maxColumn; ++i)
			{
				_leveldesigndata[i, j] = UIobjects[j * 10 + i].GetComponent<LevelButton>().returnThisState();
			}
		}
	}


	public void makeCsvButton()//ボタンプッシュで実行
	{
		makeLevelDesignData();
		makeCSV CsvCreater = new makeCSV();
		CsvCreater.logSave(datapath, _leveldesigndata);
	}
	public void makeObjectFromCsvButton()//ボタンプッシュで実行
	{
		makeDataFromCSV DataMaker = new makeDataFromCSV();
		_leveldesigndata = DataMaker.getDataElement(datapath, loadColomn - 1);
		Debug.Log("以下のcsvの列番号のデータをチェックします");
		Debug.Log(loadColomn);
		makeObject ObjectMaker = GetComponent<makeObject>();
		ObjectMaker.instanciateAllObject(_leveldesigndata, instancehight);
		goalobject = ObjectMaker.getGoalObject();
		playerobject = ObjectMaker.getPlayerObject();
		playerobject.GetComponent<CharactorMove>().setDestination(goalobject);
		updateCansetDatas(_leveldesigndata);
	}
	public void CanvasONOFFButton()//ボタンプッシュで実行
	{
		Transform trasnform = canvasObject.GetComponent<Transform>();

		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(false);
		}
	}
	public void ChangeCSVNum(Dropdown dropdown) {//保存先と、呼び出し先のcsvを変更するメソッド
		filename="testData"+dropdown.value.ToString()+".csv";
		datapath=Application.dataPath+"/data/"+filename;
		Debug.Log("changedfileto");
		Debug.Log(filename);
	}
	public void changeMapData(Vector3 aSetpos,int objectkind) {//レベルデザインデータを更新するメソッド
		Vector2 setpos = new Vector2();
		setpos = parseVector3toVector2(aSetpos);
		_leveldesigndata[(int)setpos.x, (int)setpos.y] = objectkind;
		Debug.Log(setpos.x.ToString()+setpos.y.ToString()+"ischanged to" + _leveldesigndata[(int)setpos.x, (int)setpos.y].ToString());
	}

	private Vector2 parseVector3toVector2(Vector3 aVector3) {//vector3をvector2に変換するメソッド（x→x,z→y）
		Vector2 indexpos = new Vector2();
		indexpos.x = aVector3.x;
		indexpos.y = aVector3.z;
		return indexpos;
	}
	public Vector2[] getOverRidePoint(Vector3 myposition) {//移動オブジェクトの存在する4点の座標を返すメソッド

		Vector2[] overridepoints = new Vector2[4];
		Vector2 cehckvector2 = parseVector3toVector2(myposition);
		int highx, lowx, highy, lowy;
		highx = (int)Math.Ceiling(cehckvector2.x/blocklength);
		highy = (int)Math.Ceiling(cehckvector2.y /blocklength);
		lowx = (int)Math.Floor(cehckvector2.x /blocklength);
		lowy = (int)Math.Floor(cehckvector2.y /blocklength);
		overridepoints[0].x = lowx; overridepoints[0].y = lowy;
		overridepoints[1].x = lowx; overridepoints[1].y = highy;
		overridepoints[2].x = highx; overridepoints[2].y = lowy;
		overridepoints[3].x = highx; overridepoints[3].y = highy;
		return overridepoints;
	}
	public void updateCansetDatas(int[,] existencepoints) {//ブロックが置いてあるポイントをfalseにして、それ以外をtrueにする
		for (int j = 0; j < existencepoints.GetLength(1); ++j) {
			for (int i = 0; i < existencepoints.GetLength(0); ++i) {
				if (existencepoints[i, j] == 1) {
					_canSetDatas[i, j] = false;
				}
				else { _canSetDatas[i, j] = true; }
			}
		}
	}
	public void updateCansetDatas(Vector3[] existencepoints) {//移動オブジェクトの存在するポイント4点をfalseにして、それ以外をtrueにする
		Vector2 cehckvector2; 
		foreach (var item in existencepoints) {
			cehckvector2 = parseVector3toVector2(item);
			_canSetDatas[(int)cehckvector2.x, (int)cehckvector2.y] = false;
		}
	}
	public void updateCansetDatas(Vector3 existencepoints) {
		Vector2 cehckvector2 = parseVector3toVector2(existencepoints);
		_canSetDatas[(int)existencepoints.x, (int)existencepoints.y] = false;
	}
	public bool checkCanSet(Vector3 checkvector3) {//そこにおけるかを返すメソッド。
		Vector2 checkvector2 = parseVector3toVector2(checkvector3);
		if (checkinIndex(checkvector2))
			return _canSetDatas[(int)checkvector2.x, (int)checkvector2.y];
		else { return false; }
	}
	private bool checkinIndex(Vector3 checkvector3) {
		Vector2 checkvector2 = parseVector3toVector2(checkvector3);
		return checkvector2.x >= 0 && (int)checkvector2.x < maxColumn && checkvector2.y >= 0 && (int)checkvector2.y < maxColumn;
	}


}
