using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class MassMoveDealer : MonoBehaviour
{
	MovingMass movemass;
	MathMass[,] mathmasses;
	Vector2 rightVector = new Vector2(1, 0);
	Vector2 leftVector = new Vector2(-1, 0);
	Vector2 upVector = new Vector2(0, 1);
	Vector2 downVector = new Vector2(0, -1);


	public void pushRightButton()
	{
		unusablepushButton(rightVector);
	}
	public void pushLeftButton()
	{
		unusablepushButton(leftVector);
	}
	public void pushUpButton()
	{
		unusablepushButton(upVector);
	}
	public void pushDownButton()
	{
		unusablepushButton(downVector);
	}

	private bool ismassGothrough(Vector2 directionpos)
	{
		Vector2 checkpos = movemass.GetMyPos() + directionpos;
		return mathmasses[(int)checkpos.x, (int)checkpos.y].isGoThrough();
	}

	private void unusablepushButton(Vector2 directionpos)
	{
		Vector2 checkPos = movemass.GetMyPos() + directionpos;

		if (!(ismassGothrough(checkPos)))
		{
			RenewMoverNum(mathmasses[(int)checkPos.x, (int)checkPos.y]);
		}
	}

	private void RenewMoverNum(MathMass mathmass)
	{
		int newNum = mathmass.caliculate(movemass.GetMyNumber());
		movemass.ChangeMyNum(newNum);
		mathmass.ChangeThrough();
	}
}
