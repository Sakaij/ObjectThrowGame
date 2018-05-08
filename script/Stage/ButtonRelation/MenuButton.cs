using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

public class MenuButton : ButtonBehaviour {

    public Canvas menuCanvas;

    //メニュー画面を表示させるボタンのスクリプト
    public void ManuSwitch()
    {
        if (menuCanvas.gameObject.activeSelf)
        {
            UIActiveManager.UIDisapper(menuCanvas.gameObject);
            base.Button();
        }
        else if (!menuCanvas.gameObject.activeSelf)
        {
            UIActiveManager.UIApper(menuCanvas.gameObject);
            base.Button();
        }

    }
}
