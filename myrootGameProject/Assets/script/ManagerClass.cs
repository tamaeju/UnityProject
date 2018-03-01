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
	int[,] objectTypes;//どの座標にどの種類のオブジェクトが格納されているかのデータ
	bool[,] canSet;//オブジェクトを置く事ができる座標かどうか
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
	void setAllObjectData()
	{
		for (int i = 0; i < objectTypes.GetLength(0); i++)
		{
			for (int j = 0; j < objectTypes.GetLength(1); j++)
			{
				setObjectData(i, j, 1);
			}
		}
	}
	void setObjectData(int x, int y, int prefabkind)
	{
		objectTypes[x, y] = prefabkind;
	}

	bool checkCanSet(Vector3 setpos)
	{
		return canSet[(int)setpos.x, (int)setpos.y];
	}

	int checkObjectType(Vector3 setpos)
	{
		return objectTypes[(int)setpos.x, (int)setpos.y];
	}

}
