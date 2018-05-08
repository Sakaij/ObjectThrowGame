using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
/// <summary>
/// 一番最初のキャンバスに戻るためのボタンの処理
/// </summary>
public class BackToStart : ButtonBehaviour {

    public Canvas startCanvas;//表示させたいキャンバス
    public Canvas nowCanvas; //消したいキャンバス
    public Image movieButton;//アクティブを確認するため


    public override void Button()
    {
        UIActiveManager.UIDisapper(nowCanvas.gameObject);
        UIActiveManager.UIApper(startCanvas.gameObject);
        if (movieButton.gameObject.activeSelf)
        {
            ButtonRotation.Instance.RotStart();
        }
        base.Button();
    }
}
