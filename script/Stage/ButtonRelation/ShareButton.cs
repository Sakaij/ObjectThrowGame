using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareButton : MonoBehaviour {

    //実際にシェアするテキスト
    private static string share_text = "Let's play!!"+'\n'+"#haitter"+"\n"+"ストレス解消";
    //アプリリリース時にここを追加
    private static string url_text;

    public void Share()
    {
        StartCoroutine(_Share());
    }



    IEnumerator _Share()
    {

        ScreenCapture.CaptureScreenshot("screenShot.png");
        yield return new WaitForEndOfFrame();
        string texture_url = Application.persistentDataPath + "/screenShot.png";
        SocialConnector.SocialConnector.Share(share_text, url_text);
    }
}
