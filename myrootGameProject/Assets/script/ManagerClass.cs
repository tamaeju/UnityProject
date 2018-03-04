using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

public class ManagerClass : MonoBehaviour {
	public tatchManager tachM;
	public makeObject MOclass;
	public RayEmit rayemitter;
	int[,] LevelDesignData;//どの座標にどの種類のオブジェクトが格納されているかのデータ
	public ManagerClass()
	{
	}
	void Start()
	{
	}
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{

		}

	}




	int checkObjectType(Vector3 setpos)
	{
		return LevelDesignData[(int)setpos.x, (int)setpos.y];
	}

}
