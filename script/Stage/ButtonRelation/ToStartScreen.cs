using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

public class ToStartScreen : MonoBehaviour {


    /// <summary>
    /// スタートス画面に遷移するボタン
    /// </summary>
	public void ToStartScreenButton()
    {
        MySceneManager.StartScene();
    }
}