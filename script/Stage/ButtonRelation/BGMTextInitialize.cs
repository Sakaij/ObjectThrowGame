using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;

public class BGMTextInitialize : MonoBehaviour {

    public static string onText = " BGM : ON" + "\n" + " ";
    public static string offText = " BGM : OFF" + "\n" + " ";
    public string onKey;
    public string offKey;
    public int defFontSize = 20;
    public int fontSize = 20;
	// Use this for initialization
	void Start () {
        if(TextManager.Get(onKey) == "def" && TextManager.Get(offKey) == "def")
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
        if (SaveManager.GetBGMBool())
        {
            gameObject.GetComponent<Text>().text = onText;

        }
        else
        {
            gameObject.GetComponent<Text>().text = offText;
        }

        
		
	}
	

}
