using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

public class StageSelectCanvas : ButtonBehaviour{



    public Canvas targetCanvas;
    public Canvas startCanvas;




    /// <summary>
    /// ボタンを縦に５列、横に１０列作る。
    /// </summary>
    public override void Button()
    {

        
        UIActiveManager.UIDisapper(startCanvas.gameObject);
        UIActiveManager.UIApper(targetCanvas.gameObject);
        base.Button();
        
    }




}
