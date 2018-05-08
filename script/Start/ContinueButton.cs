using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;

/// <summary>
///  続きから始めるためのボタンにつけて使用
/// </summary>
public class ContinueButton :ButtonBehaviour {


    /// <summary>
    /// 最後に遊んだステージへ遷移する
    /// </summary>
    public void StageContinue()
    {
        MySceneManager.LastStageLoad();
    }
}
