using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
public class RewardInit : MonoBehaviour {

	void Start () {
        int thisStageNumber = MySceneManager.nowStageNumber;
        if (thisStageNumber != 0)
        {
            gameObject.GetComponentInChildren<Text>().text ="+ "+ ValueManager.GetStageOfUpLife(thisStageNumber).ToString();
        }
    }
	
}
