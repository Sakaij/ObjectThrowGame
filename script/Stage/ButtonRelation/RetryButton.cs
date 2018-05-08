using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

public class RetryButton : ButtonBehaviour {


    public void Retry()
    {
        CanvasChanger.CanvasSwitch(2);
        base.Button();
    }
}
