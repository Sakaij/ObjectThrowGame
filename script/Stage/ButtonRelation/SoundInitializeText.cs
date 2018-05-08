using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

public class SoundInitializeText : MonoBehaviour
{

    public static string onText = " Sound Effects : ON"+"\n"+" ";
    public static string offText = " Sound Effects : OFF"+"\n"+" ";
    public string onKey;
    public string offKey;
    public int fontSize=20;
    public int defFontSize=20;//デフォルトのテキストを使う場合のサイズ
                           // Use this for initialization



    void Start()
    {
        if (TextManager.Get(TextManager.KEY.SOUNDBUTTONON) == "def" && TextManager.Get(TextManager.KEY.SOUNDBUTTONOFF) =="def")
        {
            gameObject.GetComponent<Text>().font = TextManager.UnipixFont;
            gameObject.GetComponent<Text>().fontSize = defFontSize;
        }
        else if (TextManager.FontBool)
        {
            gameObject.GetComponent<Text>().font = TextManager.UnipixFont;
            gameObject.GetComponent<Text>().fontSize = defFontSize;
            onText = TextManager.Get(onKey);
            offText = TextManager.Get(offKey);
        }
        else
        {
            onText = TextManager.Get(onKey);
            offText = TextManager.Get(offKey);
            gameObject.GetComponent<Text>().fontSize = fontSize;
        }
        if (SaveManager.GetSoundsBool())
        {
            gameObject.GetComponent<Text>().text = onText;
        }
        else
        {
            gameObject.GetComponent<Text>().text = offText;
        }
    }

}