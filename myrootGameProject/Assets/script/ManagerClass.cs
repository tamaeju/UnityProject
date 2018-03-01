using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

public class ManagerClass : MonoBehaviour
{
	public tatchManager tachM;
	public makeObjectClass MOclass;
	public RayEmit;
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
		return canSet[setpos.x, setpos.y];
	}

	int checkObjectType(Vector3 setpos)
	{
		return objectTypes[setpos.x, setpos.y];
	}
	//makeDataElementのメソッド
	//	int[,] practicalDataElements;
	//void parsePracticalDataElements(int[][] oldData)
	//{
	//	practicalDataElements = new int[oldData.Length, oldData[0].Length];
	//	for (int j = 0; j < oldData.Length; j++)
	//	{
	//		for (int i = 0; i < oldData[0].Length; i++)
	//		{
	//			practicalDataElements[i, j] = oldData
	//				}
	//	}
	//}
}
