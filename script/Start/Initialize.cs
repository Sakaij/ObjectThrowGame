using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagerRelation;
using UnityEngine.Advertisements;

/// <summary>
/// アプリの初期化を行う
/// </summary>
public class Initialize : MonoBehaviour {

    //これがアプリ起動後の一番最初に起動するメソッド
    private void Awake()
    {
        ScreenRotation();
        //言語設定
        TextManager.Init(Application.systemLanguage);
        //サウンドの初期化
        SoundPlayer.Init();
        BGMPlayer.Init();
        //アプリ広告初期化
        Advertisement.Initialize("1758943");
    }

    private void ScreenRotation()
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeRight;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
