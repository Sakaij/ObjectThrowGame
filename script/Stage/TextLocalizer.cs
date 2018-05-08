using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ManagerRelation;
public class TextLocalizer : MonoBehaviour {

    //呼び出したいKEY
    public string key;
    public int fontSize = 20;
    public int defFontSize = 13;

	// Use this for initialization
	void Start () {
        if (TextManager.Get(key) == "def") {
            gameObject.GetComponent<Text>().font = TextManager.UnipixFont;
            gameObject.GetComponent<Text>().fontSize = defFontSize;
            return; }
        
        if (TextManager.FontBool)
        {
            gameObject.GetComponent<Text>().font = TextManager.UnipixFont;
            gameObject.GetComponent<Text>().text = TextManager.Get(key);
            gameObject.GetComponent<Text>().fontSize = defFontSize;
            return;
        }
        gameObject.GetComponent<Text>().text = TextManager.Get(key);
        gameObject.GetComponent<Text>().fontSize = fontSize;

        
	}

}
