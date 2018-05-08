using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

public class ExplainChaceModeButton : ButtonBehaviour {



    public Button button;
    public static string chaceModeOFFText = "ChaceMode : OFF"; //ここは、デフォルトのテキスト
    public static string chaceModeONText = "ChaceMode : ON";
    public int fontSize;
    public int defFontSize;
    private static bool trackMode;

    /// <summary>
    /// 今このボタンの、テキストがONか、OFFかの状態を表すブール値(追跡モードかどうかではなくテキストの状態がどっちか）
    /// </summary>
    public static bool trackButtonBool;

    private void Start()
    {
        if (TextManager.Get(TextManager.KEY.CHACEMODEON) == "def")
        {
            button.GetComponentInChildren<Text>().font = TextManager.UnipixFont;
            button.GetComponentInChildren<Text>().fontSize = defFontSize;
        }
        else
        {
            chaceModeOFFText = TextManager.Get(TextManager.KEY.CHACEMODEOFF);
            chaceModeONText = TextManager.Get(TextManager.KEY.CHACEMODEON);
            button.GetComponentInChildren<Text>().fontSize = fontSize;
        }
        button.GetComponentInChildren<Text>().text = chaceModeOFFText;
    }

    public override void Button()
    {
        if (trackButtonBool)
        {
            button.GetComponentInChildren<Text>().text = chaceModeOFFText;
            trackButtonBool = false;
            trackMode = false;
            base.Button();
        }
        else
        {
            button.GetComponentInChildren<Text>().text = chaceModeONText;
            trackButtonBool = true;
            trackMode = true;
            if (Explain.explainedStage == 1)
            {
                button.enabled = false;
                Explain.explainedStage++;
            }
            base.Button();
        }
    }





    /// <summary>
    /// 追跡モードのboolを返す。
    /// </summary>
    /// <returns></returns>
    public static bool ChaceModeBool()
    {
        return trackMode;
    }



    /// <summary>
    /// 追跡モードをOFFにする
    /// </summary>
    public static void ChaceModeOFF()
    {
        trackMode = false;
    }
    /// <summary>
    /// 追跡モードをONにする
    /// </summary>
    public static void ChaceModeON()
    {
        trackMode = true;
    }

}
