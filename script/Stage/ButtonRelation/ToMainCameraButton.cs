using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

public class ToMainCameraButton : ButtonBehaviour {

    public Button button;
    
	// Use this for initialization

	
    //ここでは、実際に追跡モード用のカメラを動かすのではなく、ChaceModeuButtonのブールの変数をfalseにする。そうすると、メインスクリプトが感知する
    public void ToMain()
    {
        ChaceModeButton.ChaceModeOFF();
    }
}
