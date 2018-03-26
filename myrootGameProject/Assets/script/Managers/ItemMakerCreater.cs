using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakerCreater : MonoBehaviour {//Itemmakerとレフトカウントを作成するクラス
	[SerializeField]
	Meditator meditator;

	public void makeItemMaker()//この処理に関してはUIマネージャーに移譲したほうがベターかもしれない
	{
		PrefabContainer prefabcontainer = meditator.getprefabcontainer();
		DataManager datamanager = meditator.getdatamanager();
		MakeManager makemanager = meditator.getmakemanager();

		GameObject leftcountprefab = prefabcontainer.getobjectleftCount();
		GameObject dragobjectmakerprefab = prefabcontainer.getdragobjectmaker();

		for (int i = 0; i < Config.dragbuttonNum; i++) {
			float itemmakerpositiondifference = i * 3;
			float leftcountpositiondifference = i * 150;
			Transform canvastrans = prefabcontainer.getcanvasposition().transform;//キャンバスオブジェクトの値を入れて、見かけの値を入れる事で調整している。
			Vector2 leftcountpos = new Vector2(414, 115);
			Vector2 itemlabelpos = new Vector2(414, 128);

			GameObject itemleftCount = makemanager.MakeGetUIobject(leftcountprefab, leftcountpos);
			GameObject itemlabelname = makemanager.MakeGetUIobject(leftcountprefab, itemlabelpos);

			itemleftCount.transform.position = new Vector3(canvastrans.position.x + leftcountpos.x, canvastrans.position.y + leftcountpos.y - leftcountpositiondifference, itemleftCount.transform.position.z);
			itemlabelname.transform.position = new Vector3(canvastrans.position.x + itemlabelpos.x, canvastrans.position.y + itemlabelpos.y - leftcountpositiondifference, itemleftCount.transform.position.z);


			GameObject dragobjectmaker = Instantiate(dragobjectmakerprefab, dragobjectmakerprefab.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
			ItemMaker draggedobject = dragobjectmaker.GetComponent<ItemMaker>();
			Transform draggerTrans = draggedobject.transform;
			draggerTrans.position = new Vector3(draggerTrans.position.x, draggerTrans.position.y, draggerTrans.position.z - itemmakerpositiondifference);


			draggedobject.setREFofLeftCount(itemleftCount.GetComponent<Text>());
			draggedobject.setREFofItemlabel(itemlabelname.GetComponent<Text>());
			draggedobject.setMyObjectKind(datamanager.getDragitemkind(i));
			draggedobject.setObjectLeftCount(datamanager.getDragitemleft(i));

		}
	}
}
