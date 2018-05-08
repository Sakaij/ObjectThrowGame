using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
using SocialConnector;



public class TwitterButton : ButtonBehaviour {
    public static string explanationText= "投げるだけ！超爽快投げ系ゲーム!"+'\n'+"インストールしよう!!";
    public static string urlText = "Android URL : " +"a";
    public static string hashText = "#新感覚投げ系ゲーム";
    public static string hashText2 = "#a";

    public override void Button()
    {

        StartCoroutine(ShareScreenShot());
        base.Button();
        
        
    }

    IEnumerator ShareScreenShot()
    {
        //スクリーンショット画像の保存先を設定。ファイル名が重複しないように実行時間を付与
        string fileName = String.Format("image_{0:yyyyMMdd_Hmmss}.png", DateTime.Now);
        string imagePath = Application.persistentDataPath + "/" + fileName;

        //スクリーンショットを撮影
        ScreenCapture.CaptureScreenshot(fileName);
        yield return new WaitForEndOfFrame();

        // Shareするメッセージを設定
        string text = explanationText+'\n'+'\n'+urlText+'\n'+hashText;
        string URL = "url";
        yield return new WaitForSeconds(1);
        SocialConnector.SocialConnector.Share(explanationText+'\n'+'\n'+urlText+'\n'+'\n'+hashText+'\n'+hashText2, "");
    }



}
