using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakerCreater : MonoBehaviour { //Itemmakerとレフトカウントを作成するクラス
	[SerializeField]
	Meditator meditator;
	[SerializeField]
	StageUIMaker UImaker;
	[SerializeField]
	GameObject[] instancepos;

	public void makeItemMaker () //この処理に関してはUIマネージャーに移譲したほうがベターかもしれない
	{
		PrefabContainer prefabcontainer = meditator.getprefabcontainer ();
		DataStorage dataholder = meditator.getdataholder ();
		MapEditorUIManager uimanager = meditator.getUImanager ();

		GameObject leftcountprefab = prefabcontainer.getobjectleftCount ();
		GameObject dragobjectmakerprefab = prefabcontainer.getdragobjectmaker ();
		//アイテムの残数を表示する部分

		for (int i = 0; i < Config.dragbuttonNum; i++) {
			GameObject instanceOB = Instantiate (dragobjectmakerprefab, instancepos[i].transform.position, Quaternion.Euler (0, 0, 180), instancepos[i].transform) as GameObject;
			ItemMaker MakerObject = instanceOB.GetComponent<ItemMaker> ();
			MakerObject.setMyObjectKind (dataholder.GetDragItemElement () [i].itemkind);
			MakerObject.setObjectLeftCount (dataholder.GetDragItemElement () [i].itemcount);
			MakerObject.changeMyTexture (i);
			UImaker.makeItemUI (MakerObject.getMyKind (), MakerObject.ObjectLeftCount, (int) StageUIMaker.displayposition.rightupper + i);
		}
	}
}