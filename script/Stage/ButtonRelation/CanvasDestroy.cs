using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

public class CanvasDestroy : ButtonBehaviour {

    public Canvas destroyCanvas;

    public override void Button()
    {
        UIActiveManager.UIDisapper(destroyCanvas.gameObject);

        base.Button();
    }
}
