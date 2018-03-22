using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MassDealer : MonoBehaviour {
	[SerializeField]
	Meditator meditator;

	public Vector2 parseVector3XYZtoVector2XZ(Vector3 aVector3)
	{//vector3をvector2に変換するメソッド（x→x,z→y）
		Vector2 indexpos = new Vector2();
		indexpos.x = aVector3.x;
		indexpos.y = aVector3.z;
		return indexpos;
	}
	public Vector3 getIndexpos(Vector3 aPos)
	{//設置されているポジションのindexを返すメソッド
		Vector3 indexpos = new Vector3();
		indexpos.x = (float)Math.Round(aPos.x / Config.blocklength);
		//indexpos.y = (float)Math.Round(aPos.y / Config.blocklength);//yの座標を返す必要がないのでコメントアウト
		indexpos.y = aPos.y;
		indexpos.z = (float)Math.Round(aPos.z / Config.blocklength);
		return indexpos;
	}
	public Vector3 getRoundedgPos(Vector3 aPos)
	{//設置されるポジションを返すメソッド.getIndexPosにブロックのlengthをかけて、インデックスを実際に使えるvectorに変換している。
		Vector3 roundedpos = new Vector3();
		roundedpos.x = getIndexpos(aPos).x * Config.blocklength;
		//roundedpos.y = getIndexpos(aPos).y * Config.blocklength;//yの座標を返す必要がないのでコメントアウト
		roundedpos.y = aPos.y;
		roundedpos.z = getIndexpos(aPos).z * Config.blocklength;
		return roundedpos;
	}
	public Vector3 getInstanceposFromMouse(int slideypos)//オブジェクトを配置する際のメソッド。意図的にy座標は固定している
	{
		Vector3 screenPotsition, instancePosition;
		screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
		instancePosition.y = meditator.getmakemanager().getObjecthight() + slideypos;
		return instancePosition;
	}
	public Vector3 getposFromMouse()//オブジェクトを配置する際のメソッド。意図的にy座標は固定している
{
		Vector3 screenPotsition, instancePosition;
		screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.z, Input.mousePosition.y);
		instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
		return instancePosition;
	}
}
