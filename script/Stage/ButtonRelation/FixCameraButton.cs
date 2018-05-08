using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraUtil;
using ManagerRelation;

public class FixCameraButton : ButtonBehaviour {

    public Button button;
    public Button rightButton;
    public Button leftButton;
    public Button trackModeButton;
    public Scrollbar positionChangeBar; //カメラの位置を変更するためのスクロールバー

	
    public override void Button()
    {

        //すでに固定カメラが入っているときの処理　メインカメラに戻すので、追跡モード用のボタンをONにしておく
        if (MyCameraSwitch.nowCamera == 2 | MyCameraSwitch.nowCamera == 3)
        {
            MyCameraSwitch.CameraChanger(1);
            UIActiveManager.UIDisapper(rightButton.gameObject, leftButton.gameObject , positionChangeBar.gameObject); // 固定左右ボタンとスクロールバーを非表示
            LeftAndRightButton.cameraNumber = 0; //左右固定カメラが入っていないことを伝えておく。
            UIActiveManager.UIApper(trackModeButton.gameObject); //追跡カメラ用のボタンを表示
            base.Button();
        }
        else
        {
            MyCameraSwitch.CameraChanger(2); //右に入れる
            positionChangeBar.value = 1;
            UIActiveManager.UIApper(leftButton.gameObject,  rightButton.gameObject, positionChangeBar.gameObject); //固定右カメラと、スクロールバーを表示
            LeftAndRightButton.cameraNumber = 1; //右カメラだという情報を置いておく。
            UIActiveManager.UIDisapper(trackModeButton.gameObject); //追跡カメラ用のボタンは非表示
            base.Button();
        }
    }





}
