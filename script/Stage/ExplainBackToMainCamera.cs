using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplainBackToMainCamera : ButtonBehaviour {
    public Button button;
    /// <summary>
    /// メインカメラに変えるためのボタンのテキスト。
    /// </summary>
    public static string toMainButtonText = "Back";

    // Use this for initialization
    void Start()
    {
        button.GetComponentInChildren<Text>().text = toMainButtonText;
    }

    //ここでは、実際に追跡モード用のカメラを動かすのではなく、ChaceModeuButtonのブールの変数をfalseにする。そうすると、メインスクリプトが感知する
    public void ToMain()
    {
        ExplainChaceModeButton.ChaceModeOFF();
        Explain.explainedStage++;
        base.Button();
    }
}
