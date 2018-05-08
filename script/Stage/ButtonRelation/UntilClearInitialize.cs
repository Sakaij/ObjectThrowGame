using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

public class UntilClearInitialize : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        int thisStageNumber = MySceneManager.nowStageNumber;
        if (thisStageNumber != 0)
        {
            gameObject.GetComponentInChildren<Text>().text = ValueManager.UntilStageClear(thisStageNumber).ToString();
        }
    }
}
