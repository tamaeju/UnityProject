using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

public class setObject
{

	Vector3 mypos;
	float posZ = 0f;
	public setObject(int x,int y)
	{
		mypos.x = x;
		mypos.y = y;
		mypos.z = posZ;
	}

	public Vector3 returnPos() {
		return mypos;
	}

}
