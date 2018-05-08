using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

/// <summary>
///メニューのステージ詳細で現在のステージ数を表示するのに使用
/// </summary>
public class StageNumberSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Text>().text =MySceneManager.nowStageNumber.ToString();
	}

}
