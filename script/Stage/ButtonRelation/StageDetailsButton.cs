using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
public class StageDetailsButton : ButtonBehaviour {

    public Image detailsCanvas;

    //ステージの詳細を出す。
    public void ApperDetails()
    {
        if (!detailsCanvas.gameObject.activeSelf)
        {
            UIActiveManager.UIApper(detailsCanvas.gameObject);
            base.Button();

        }else
        {
            UIActiveManager.UIDisapper(detailsCanvas.gameObject);
            base.Button();
        }
    }
	
}
