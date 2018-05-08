using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraUtil;

public class LeftAndRightButton : ButtonBehaviour {

    /// <summary>
    /// 1ならば右カメラ、２ならば左カメラ ０ならば固定カメラ以外のカメラが入っていることを表す。
    /// </summary>
    public static int cameraNumber;
    private static bool buttonActiveBool = false;



    /// <summary>
    /// 左の固定カメラに変更
    /// </summary>
    public void LeftButton()
    {
        MyCameraSwitch.CameraChanger(3);
        cameraNumber = 2;
        CameraScroll.scrollbar.value = 0;
        base.Button();
    }


    /// <summary>
    /// 右の固定カメラに変更
    /// </summary>
   public void RightButton()
    {
        MyCameraSwitch.CameraChanger(2);
        cameraNumber = 1;
        CameraScroll.scrollbar.value = 1;
        base.Button();
    }





    
}


