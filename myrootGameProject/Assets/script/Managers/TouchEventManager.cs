using UnityEngine;
using System.Collections;
using System;

public class TouchEventManager : MonoBehaviour {
	GameObject refObject;
	Vector3 instancePosition;
	RayEmit rayemitter;
	ItemMaker draggeeditem;

	[SerializeField]
	Meditator meditator;
	MakeManager makemanager;
	DataManager datamanager;
	DataCheck datachecker;
	MassDealer massdealer;



	void Start() {
		makemanager = meditator.getmakemanager();
		datamanager = meditator.getdatamanager();
		datachecker = meditator.getdatachecker();
		massdealer = meditator.getmassdealer();
		rayemitter = new RayEmit();
	}


	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			try { draggeeditem = rayemitter.getObject().GetComponent<ItemMaker>(); }
			catch { Debug.Log(string.Format("draggeeditem が null)", draggeeditem)); }
			if (draggeeditem !=null&& draggeeditem.GetType() == typeof(ItemMaker) && draggeeditem.getObjectLeftCount() > 0) 
				{//ドラッグしたアイテムがmakedraggedobjectであり、かつレフトカウントが0より大きいなら
				int prefabkind = draggeeditem.getMyObjectKind();
				refObject = makemanager.InstanciateandGetRef(prefabkind, massdealer.getInstanceposFromMouse(2));
			}
			else {
				Debug.Log(String.Format("draggeeditem.GetType() is {0} draggeeditem.getObjectLeftCount() is {1}", draggeeditem.GetType(), draggeeditem.getObjectLeftCount()));
				Debug.Log("touched irregular obeject or noLeftItem");
			}
		}

		if (Input.GetMouseButton(0)) {//タッチされ続けている間、オブジェクトの位置を動かし続ける。
			if (refObject != null) {
				refObject.transform.position = massdealer.getInstanceposFromMouse(2);
			}
			else {
			}
		}

		if (Input.GetMouseButtonUp(0)) {//タッチが離されたタイミングで、オブジェクトをつかんでいればnullを入れる。
			instancePosition =massdealer.getInstanceposFromMouse(0);
			Vector3 indexVector3 = massdealer.getIndexpos(instancePosition);//x,y,zが何番目の配列か調べる。
			if (datachecker.checkCanSet(indexVector3)&& refObject!=null) {//今のポジションのインデックスが配列内であり、セットできるのであれば
				draggeeditem.decreaseLeftCount();//レフトカウントを1減らす。
				refObject.transform.position = massdealer.getRoundedgPos(instancePosition);
				datamanager.changeMapData(indexVector3, draggeeditem.getMyObjectKind());
				datamanager.updateCansetDatas(indexVector3);
			}
			else {
				Destroy(refObject);
			}
			refObject = null;
			draggeeditem = null;
		}
	}

}