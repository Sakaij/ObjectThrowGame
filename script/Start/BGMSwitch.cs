using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

/// <summary>
/// スタート画面のBGM切り替え用のボタンにつけて使用
/// </summary>
public class BGMSwitch : ButtonBehaviour {

    public Sprite bgmOnImage;
    public Sprite bgmOffImage;


    private void Start()
    {
        if (!SaveManager.GetBGMBool())
        {
            gameObject.GetComponent<Image>().sprite = bgmOffImage;
        }
    }

    public override void Button() {
        if (SaveManager.GetBGMBool())
        {
            gameObject.GetComponent<Image>().sprite = bgmOffImage;
            SaveManager.SetBGMBool(false);
            BGMPlayer.Instance.StopBGM();
            base.Button();
            return;
        }
        gameObject.GetComponent<Image>().sprite = bgmOnImage;
        SaveManager.SetBGMBool(true);
        BGMPlayer.Instance.StartBGM(BGMPlayer.startBGM);
        base.Button();

    }


}
