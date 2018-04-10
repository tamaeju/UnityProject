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
	GameObject movemass;
	GameObject [,] mathmasses;
	Vector2 rightVector = new Vector2(1, 0);
	Vector2 leftVector = new Vector2(-1, 0);
	Vector2 upVector = new Vector2(0, 1);
	Vector2 downVector = new Vector2(0, -1);
	[SerializeField]
	CurrentStageData currentdata;
	[SerializeField]
	FieldObjectMaker fieldobjectmaker;

	//4秒で1回転するロジック、サイン関数を用いた実装。

	private void LoadFieldObject() {
		movemass =  fieldobjectmaker.GetMovingMass();
		mathmasses = fieldobjectmaker.GetMathMasses();
	}

	private void unusablepushButton(Vector2 directionpos)
	{
		Vector2 checkPos = movemass.GetComponent<MovingMass>().GetMyPos() + directionpos;

		if (!(mathmasses[(int)checkPos.x, (int)checkPos.y].GetComponent<MathMass>().isGoThrough()))
		{
			RenewMoverNum(mathmasses[(int)checkPos.x, (int)checkPos.y].GetComponent<MathMass>());
		}
	}

	private void RenewMoverNum(MathMass mathmass)
	{
		int newNum = mathmass.caliculate(movemass.GetComponent<MovingMass>().GetMyNumber());
		movemass.GetComponent<MovingMass>().ChangeMyNum(newNum);
		mathmass.ChangeThrough();
	}

	public void pushRightButton() {
		unusablepushButton(rightVector);
	}
	public void pushLeftButton() {
		unusablepushButton(leftVector);
	}
	public void pushUpButton() {
		unusablepushButton(upVector);
	}
	public void pushDownButton() {
		unusablepushButton(downVector);
	}
	public void pushBackButton() {
	}


}

//private MathMass[,] ComvertMathsmassObjectType(GameObject[,] gotmathmass) {
//	MathMass[,] newMathmass = new MathMass[gotmathmass.GetLength(0), gotmathmass.GetLength(1)];
//	foreach (var item in gotmathmass) {
//		newMathmass
//		}
//	return
//	}
