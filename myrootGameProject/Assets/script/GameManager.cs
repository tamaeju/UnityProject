using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{//データ保持とUIの作成と保持

	[SerializeField] GameObject DMobject;
	[SerializeField] GameObject UIMobject;
	[SerializeField]GameObject Makeobject;
	[SerializeField]GameObject touchobject;
	DataManager datamanager;
	UIManager UImanager;
	MakeManager makemanager;
	TouchManager touchmanager;


	void Start(){
		datamanager = (Instantiate(DMobject) as GameObject).GetComponent<DataManager>();
		UImanager = (Instantiate(UIMobject) as GameObject).GetComponent<UIManager>();
		makemanager = (Instantiate(DMobject) as GameObject).GetComponent<MakeManager>();
		touchmanager = (Instantiate(UIMobject) as GameObject).GetComponent<TouchManager>();
	}




}
