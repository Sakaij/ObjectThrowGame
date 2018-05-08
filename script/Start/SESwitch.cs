using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;


/// <summary>
/// スタート画面の効果音のONOFFを切り替えるときに使用するクラス
/// </summary>
public class SESwitch : ButtonBehaviour
{
    public Image soundImage;
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    // Use this for initialization
    void Start()
    {
        if (!SaveManager.GetSoundsBool())
        {
            //サウンドがOFFの時用のSpriteに入れかえ
            soundImage.sprite = soundOffImage;

        }
    }

    //
    public override void Button()
    {
        if (SaveManager.GetSoundsBool())
        {
            soundImage.sprite = soundOffImage;
            SaveManager.SetSoundsBool(false);
            base.Button();
            return;
        }
        soundImage.sprite = soundOnImage;
        SaveManager.SetSoundsBool(true);
        base.Button();

    }
}